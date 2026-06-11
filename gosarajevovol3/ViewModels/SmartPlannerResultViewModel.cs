namespace gosarajevovol3.ViewModels
{
    public class SmartPlannerResultViewModel
    {
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public int NumberOfDays { get; set; }
        public Dictionary<int, List<PlannerLocationViewModel>> PlanByDay { get; set; } = new();
        public string? StartAddress { get; set; }
        public double? StartLat { get; set; }
        public double? StartLng { get; set; }
    }
}
