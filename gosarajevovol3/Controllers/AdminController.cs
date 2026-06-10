using gosarajevovol3.Data;
using gosarajevovol3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace gosarajevovol3.Controllers;

[Authorize]
[ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<User> _userManager;

    public AdminController(ApplicationDbContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var currentUser = await _userManager.GetUserAsync(User);
        if (currentUser is not Admin)
        {
            context.Result = new ForbidResult();
            return;
        }
        await next();
    }
    public async Task<IActionResult> Dashboard()
    {
        ViewBag.TotalUsers = await _context.RegisteredUsers.CountAsync();
        ViewBag.TotalAttractions = await _context.Attractions.CountAsync();
        ViewBag.TotalEvents = await _context.Events.CountAsync();
        ViewBag.TotalReviews = await _context.Reviews.CountAsync();
        ViewBag.Attractions = await _context.Attractions.ToListAsync();
        ViewBag.Hospitality = await _context.Hospitality.ToListAsync();
        ViewBag.Events = await _context.Events.ToListAsync();
        ViewBag.Reviews = await _context.Reviews.Include(r => r.RegisteredUser).ToListAsync();

        return View();
    }
    public async Task<IActionResult> DeleteAttraction(int id)
    {
        var attraction = await _context.Attractions.FindAsync(id);
    
        if (attraction != null)
        {
            _context.Attractions.Remove(attraction);
            await _context.SaveChangesAsync(); 
        }
        return RedirectToAction("Dashboard");
    }
    [HttpGet]
    public IActionResult CreateAttraction()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateAttraction(Attraction model, double latitude, double longitude)
    {
        var koordinate = new Coordinates { Latitude = latitude, Longitude = longitude };
        model.Coordinates = koordinate;
        ModelState.Remove("Coordinates");

        if (ModelState.IsValid)
        {
            _context.Attractions.Add(model);
            await _context.SaveChangesAsync(); 
            return RedirectToAction("Dashboard");
        }

        return View(model);
    }
    [HttpGet]
    public async Task<IActionResult> EditAttraction(int id)
    {
        var attraction = await _context.Attractions
            .Include(a => a.Coordinates)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (attraction == null)
        {
            return NotFound();
        }

        return View(attraction);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditAttraction(int id, Attraction model, double latitude, double longitude)
    {
        if (id != model.Id) return NotFound();
        var uBazi = await _context.Attractions
            .Include(a => a.Coordinates)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (uBazi == null) return NotFound();

        ModelState.Remove("Coordinates");

        if (ModelState.IsValid)
        {
            uBazi.AttractionName = model.AttractionName;
            uBazi.AttractionDescription = model.AttractionDescription;
            uBazi.PhotoUrl = model.PhotoUrl;
            uBazi.AttractionType = model.AttractionType;
            uBazi.LocationAddress = model.LocationAddress;
            uBazi.Coordinates.Latitude = latitude;
            uBazi.Coordinates.Longitude = longitude;

            await _context.SaveChangesAsync(); 
            return RedirectToAction("Dashboard");
        }

        return View(model);
    }
    [HttpGet]
    public IActionResult CreateHospitality()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateHospitality(Hospitality model, double latitude, double longitude)
    {
        var koordinate = new Coordinates { Latitude = latitude, Longitude = longitude };
        model.Coordinates = koordinate;
        ModelState.Remove("Coordinates");
        ModelState.Remove("Reviews");

        if (ModelState.IsValid)
        {
            _context.Hospitality.Add(model);
            await _context.SaveChangesAsync(); 
            return RedirectToAction("Dashboard");
        }

        return View(model);
    }
    [HttpGet]
    public async Task<IActionResult> EditHospitality(int id)
    {
        var hospitality = await _context.Hospitality
            .Include(h => h.Coordinates)
            .FirstOrDefaultAsync(h => h.Id == id);

        if (hospitality == null) return NotFound();

        return View(hospitality);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditHospitality(int id, Hospitality model, double latitude, double longitude)
    {
        if (id != model.Id) return NotFound();

        var uBazi = await _context.Hospitality
            .Include(h => h.Coordinates)
            .FirstOrDefaultAsync(h => h.Id == id);

        if (uBazi == null) return NotFound();

        ModelState.Remove("Coordinates");
        ModelState.Remove("Reviews");

        if (ModelState.IsValid)
        {
            uBazi.HospitalityName = model.HospitalityName;
            uBazi.HospitalityDescription = model.HospitalityDescription;
            uBazi.PhotoUrl = model.PhotoUrl;
            uBazi.HospitalityType = model.HospitalityType;
            uBazi.LocationAddress = model.LocationAddress;
            uBazi.GooglePlaceId = model.GooglePlaceId;
            uBazi.Coordinates.Latitude = latitude;
            uBazi.Coordinates.Longitude = longitude;

            await _context.SaveChangesAsync(); 
            return RedirectToAction("Dashboard");
        }

        return View(model);
    }

    public async Task<IActionResult> DeleteHospitality(int id)
    {
        var hospitality = await _context.Hospitality.FindAsync(id);
        if (hospitality != null)
        {
            _context.Hospitality.Remove(hospitality);
            await _context.SaveChangesAsync(); 
        } 
        return RedirectToAction("Dashboard");
    }
}