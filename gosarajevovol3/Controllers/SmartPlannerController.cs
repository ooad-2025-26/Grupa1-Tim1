using gosarajevovol3.Data;
using gosarajevovol3.Models;
using gosarajevovol3.ViewModels;
using gosarajevovol2.Models.Enums;
using gosarajevovol3.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace gosarajevovol3.Controllers;

public class SmartPlannerController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;
    private const int LocationsPerDay = 5;
    private const int CandidateTierSize = 6; // koliko "jednako dobrih" gledamo prije izbora po blizini

    public SmartPlannerController(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public IActionResult Index()
    {
        if (User.Identity?.IsAuthenticated != true)
            return View("NotRegistered");

        bool isRegistered = _context.RegisteredUsers
            .Any(u => u.Email == User.Identity.Name);
        if (!isRegistered)
            return View("NotRegistered");

        var model = new SmartPlannerInputViewModel();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> GenerirajPlan(SmartPlannerInputViewModel input)
    {
        if (User.Identity?.IsAuthenticated != true)
            return View("NotRegistered");

        bool isRegistered = await _context.RegisteredUsers
            .AnyAsync(u => u.Email == User.Identity.Name);
        if (!isRegistered)
            return View("NotRegistered");
        if (!ModelState.IsValid)
            return View("Index", input);

        if (!input.DatesAreValid)
        {
            ModelState.AddModelError("DepartureDate", "Departure date must be after the arrival date.");
            return View("Index", input);
        }

        var attractions = await _context.Attractions
            .Include(a => a.Coordinates)
            .ToListAsync();

        var hospitality = await _context.Hospitality
            .Include(h => h.Coordinates)
            .Where(h => h.HospitalityType == HospitalityType.Restaurant ||
                        h.HospitalityType == HospitalityType.Bars ||
                        h.HospitalityType == HospitalityType.Museums)
            .ToListAsync();

        var events = await _context.Events
            .Include(e => e.Coordinates)
            .ToListAsync();

        var result = new SmartPlannerResultViewModel
        {
            ArrivalDate = input.ArrivalDate,
            DepartureDate = input.DepartureDate,
            NumberOfDays = input.NumberOfDays
        };

        result.PlanByDay = GeneratePlan(input, attractions, hospitality, events);

        ViewBag.GoogleMapsApiKey = _configuration["GoogleMaps:ApiKey"];

        return View("Result", result);
    }

    private class Candidate
    {
        public PlannerLocationViewModel Vm { get; set; } = null!;
        public int Score { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public bool IsAttraction { get; set; }
        public bool IsBar { get; set; }
        public bool IsEvent { get; set; }
        public int SourceId { get; set; }
        public DateTime? EventStart { get; set; }
    }

    private int GetDailyTypeLimit(string attractionType, SmartPlannerInputViewModel input)
    {
        return attractionType switch
        {
            "Nature" => input.NatureScore >= 4 ? 2 : 1,
            "Historical" => 2,
            "Religious" => 2,
            "Monument" => 2,
            _ => LocationsPerDay
        };
    }

    private Dictionary<int, List<PlannerLocationViewModel>> GeneratePlan(
        SmartPlannerInputViewModel input,
        List<Attraction> attractions,
        List<Hospitality> hospitality,
        List<Event> events)
    {
        var plan = new Dictionary<int, List<PlannerLocationViewModel>>();
        var usedAttractionIds = new HashSet<int>();
        var usedHospitalityIds = new HashSet<int>();
        var usedEventIds = new HashSet<int>();

        int GetAttractionScore(Attraction a) => a.AttractionType switch
        {
            AttractionType.Historical => input.HistoryScore,
            AttractionType.Religious => input.HistoryScore,
            AttractionType.Monument => input.CultureScore,
            AttractionType.Nature => input.NatureScore,
            AttractionType.Other => 3,
            _ => 3
        };

        int GetHospitalityScore(Hospitality h) => h.HospitalityType switch
        {
            HospitalityType.Restaurant => input.FoodScore,
            HospitalityType.Bars => input.NightlifeScore,
            HospitalityType.Museums => input.CultureScore,
            _ => 0
        };

        int GetEventScore(Event e) => e.Type switch
        {
            EventType.Concert => input.NightlifeScore,
            EventType.Theater => input.CultureScore,
            EventType.Movie => input.CultureScore,
            EventType.Festival => Math.Max(input.CultureScore, input.NightlifeScore),
            EventType.Other => 2,
            _ => 2
        };

        Candidate ToAttractionCandidate(Attraction a) => new Candidate
        {
            Score = GetAttractionScore(a),
            Lat = a.Coordinates.Latitude,
            Lng = a.Coordinates.Longitude,
            IsAttraction = true,
            IsBar = false,
            IsEvent = false,
            SourceId = a.Id,
            Vm = new PlannerLocationViewModel
            {
                Name = a.AttractionName,
                Description = a.AttractionDescription,
                ImageUrl = a.PhotoUrl,
                Category = "Attraction",
                Type = a.AttractionType.ToString(),
                Latitude = a.Coordinates.Latitude,
                Longitude = a.Coordinates.Longitude
            }
        };

        Candidate ToHospitalityCandidate(Hospitality h) => new Candidate
        {
            Score = GetHospitalityScore(h),
            Lat = h.Coordinates.Latitude,
            Lng = h.Coordinates.Longitude,
            IsAttraction = false,
            IsBar = h.HospitalityType == HospitalityType.Bars,
            IsEvent = false,
            SourceId = h.Id,
            Vm = new PlannerLocationViewModel
            {
                Name = h.HospitalityName,
                Description = h.HospitalityDescription,
                ImageUrl = h.PhotoUrl,
                Category = "Hospitality",
                Type = h.HospitalityType.ToString(),
                Latitude = h.Coordinates.Latitude,
                Longitude = h.Coordinates.Longitude
            }
        };

        Candidate ToEventCandidate(Event e) => new Candidate
        {
            Score = GetEventScore(e),
            Lat = e.Coordinates.Latitude,
            Lng = e.Coordinates.Longitude,
            IsAttraction = false,
            IsBar = e.Type == EventType.Concert,
            IsEvent = true,
            SourceId = e.Id,
            EventStart = e.StartDate,
            Vm = new PlannerLocationViewModel
            {
                Name = string.IsNullOrEmpty(e.EventNameEn)
    ? e.EventName
    : e.EventNameEn,

                Description = string.IsNullOrEmpty(e.EventDescriptionEn)
    ? e.EventDescription
    : e.EventDescriptionEn,
                ImageUrl = e.PhotoUrl,
                Category = "Event",
                Type = e.Type.ToString(),
                Latitude = e.Coordinates.Latitude,
                Longitude = e.Coordinates.Longitude
            }
        };

        for (int day = 1; day <= input.NumberOfDays; day++)
        {
            var currentDate = input.ArrivalDate.Date.AddDays(day - 1);

            int hospitalitySlots = CalculateHospitalitySlots(input);
            int attractionSlots = LocationsPerDay - hospitalitySlots;

            var attrPool = attractions
                .Where(a => !usedAttractionIds.Contains(a.Id))
                .Select(ToAttractionCandidate)
                .OrderByDescending(c => c.Score)
                .ThenBy(_ => Guid.NewGuid())
                .ToList();

            var barPool = hospitality
                .Where(h => !usedHospitalityIds.Contains(h.Id) &&
                            h.HospitalityType == HospitalityType.Bars)
                .Select(ToHospitalityCandidate)
                .OrderByDescending(c => c.Score)
                .ThenBy(_ => Guid.NewGuid())
                .ToList();

            var hospPool = hospitality
                .Where(h => !usedHospitalityIds.Contains(h.Id) &&
                            h.HospitalityType != HospitalityType.Bars)
                .Select(ToHospitalityCandidate)
                .OrderByDescending(c => c.Score)
                .ThenBy(_ => Guid.NewGuid())
                .ToList();

            var eventPool = events
                .Where(e => !usedEventIds.Contains(e.Id) &&
                            e.StartDate.Date <= currentDate &&
                            e.EndDate.Date >= currentDate &&
                            GetEventScore(e) >= 3)
                .Select(ToEventCandidate)
                .OrderByDescending(c => c.Score)
                .ThenBy(c => c.EventStart)
                .ToList();

            var dayLocations = new List<PlannerLocationViewModel>();
            double? lastLat = null;
            double? lastLng = null;
            var typeCountToday = new Dictionary<string, int>();

            bool IsOverTypeLimit(Candidate c)
            {
                return typeCountToday.GetValueOrDefault(c.Vm.Type, 0) >=
                       GetDailyTypeLimit(c.Vm.Type, input);
            }

            Candidate? PickNext(List<Candidate> pool, bool respectTypeLimit = true)
            {
                if (pool.Count == 0)
                    return null;

                var available = respectTypeLimit
                    ? pool.Where(c => !IsOverTypeLimit(c)).ToList()
                    : pool.ToList();

                if (available.Count == 0)
                    return null;

                if (lastLat == null || lastLng == null)
                    return available[0];

                var tier = available.Take(CandidateTierSize).ToList();

                return tier
                    .OrderBy(c => Haversine(lastLat.Value, lastLng.Value, c.Lat, c.Lng))
                    .First();
            }

            void Commit(Candidate c)
            {
                dayLocations.Add(c.Vm);

                lastLat = c.Lat;
                lastLng = c.Lng;

                if (c.IsEvent)
                    usedEventIds.Add(c.SourceId);
                else if (c.IsAttraction)
                    usedAttractionIds.Add(c.SourceId);
                else
                    usedHospitalityIds.Add(c.SourceId);

                attrPool.RemoveAll(x => x.SourceId == c.SourceId && x.IsAttraction);
                hospPool.RemoveAll(x => x.SourceId == c.SourceId && !x.IsAttraction && !x.IsEvent);
                barPool.RemoveAll(x => x.SourceId == c.SourceId && !x.IsAttraction && !x.IsEvent);
                eventPool.RemoveAll(x => x.SourceId == c.SourceId && x.IsEvent);

                var t = c.Vm.Type;
                typeCountToday[t] = typeCountToday.GetValueOrDefault(t, 0) + 1;
            }

            Candidate? todaysEvent = PickNext(eventPool, respectTypeLimit: false);

            bool addBarToday = input.NightlifeScore >= 2 && barPool.Count > 0;

            if (todaysEvent != null && todaysEvent.IsBar && input.NightlifeScore < 5)
                addBarToday = false;

            int reservedEventSlot = todaysEvent != null ? 1 : 0;
            int reservedBarSlot = addBarToday ? 1 : 0;

            var anchor = PickNext(attrPool) ?? PickNext(hospPool);
            if (anchor != null)
                Commit(anchor);

            int attrFilled = anchor != null && anchor.IsAttraction ? 1 : 0;
            int hospFilled = anchor != null && !anchor.IsAttraction ? 1 : 0;

            int regularHospSlots = addBarToday ? hospitalitySlots - 1 : hospitalitySlots;
            if (regularHospSlots < 0)
                regularHospSlots = 0;

            while (dayLocations.Count < LocationsPerDay - reservedEventSlot - reservedBarSlot)
            {
                bool needAttraction = attrFilled < attractionSlots && attrPool.Count > 0;
                bool needHospitality = hospFilled < regularHospSlots && hospPool.Count > 0;

                Candidate? next = null;

                if (needAttraction && needHospitality)
                {
                    var bestA = PickNext(attrPool);
                    var bestH = PickNext(hospPool);

                    double dA = bestA != null && lastLat != null && lastLng != null
                        ? Haversine(lastLat.Value, lastLng.Value, bestA.Lat, bestA.Lng)
                        : double.MaxValue;

                    double dH = bestH != null && lastLat != null && lastLng != null
                        ? Haversine(lastLat.Value, lastLng.Value, bestH.Lat, bestH.Lng)
                        : double.MaxValue;

                    next = dA <= dH ? bestA : bestH;
                }
                else if (needAttraction)
                {
                    next = PickNext(attrPool);
                }
                else if (needHospitality)
                {
                    next = PickNext(hospPool);
                }
                else
                {
                    next = PickNext(attrPool, respectTypeLimit: false)
                        ?? PickNext(hospPool, respectTypeLimit: false);
                }

                if (next == null)
                    break;

                Commit(next);

                if (next.IsAttraction)
                    attrFilled++;
                else
                    hospFilled++;
            }

            if (addBarToday)
            {
                var bar = PickNext(barPool, respectTypeLimit: false);
                if (bar != null)
                    Commit(bar);
            }

            if (todaysEvent != null && !usedEventIds.Contains(todaysEvent.SourceId))
            {
                int hour = todaysEvent.EventStart?.Hour ?? 20;

                if (hour == 0)
                    hour = 20;

                int position;

                if (hour < 12)
                    position = 0;
                else if (hour < 17)
                    position = Math.Min(2, dayLocations.Count);
                else
                    position = Math.Min(4, dayLocations.Count);

                if (dayLocations.Count >= LocationsPerDay)
                {
                    if (position < dayLocations.Count)
                        dayLocations.RemoveAt(position);
                    else
                        dayLocations.RemoveAt(dayLocations.Count - 1);
                }

                dayLocations.Insert(position, todaysEvent.Vm);
                usedEventIds.Add(todaysEvent.SourceId);
            }
            plan[day] = dayLocations;
        }
            return plan;
    }

    private int CalculateHospitalitySlots(SmartPlannerInputViewModel input)
    {
        int totalScore = input.HistoryScore + input.NatureScore +
                         input.CultureScore + input.FoodScore + input.NightlifeScore;

        int hospitalityScore = input.FoodScore + input.NightlifeScore;

        if (totalScore == 0)
            return 1;

        double ratio = (double)hospitalityScore / totalScore;
        int slots = (int)Math.Round(ratio * LocationsPerDay);

        return Math.Clamp(slots, 1, 3);
    }

    private static double Haversine(double lat1, double lon1, double lat2, double lon2)
    {
        const double R = 6371000.0;

        double dLat = (lat2 - lat1) * Math.PI / 180.0;
        double dLon = (lon2 - lon1) * Math.PI / 180.0;

        double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                   Math.Cos(lat1 * Math.PI / 180.0) *
                   Math.Cos(lat2 * Math.PI / 180.0) *
                   Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        return R * c;
    }
}
