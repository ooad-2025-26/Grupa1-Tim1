namespace gosarajevovol3.ViewModels
{
    public class PlannerLocationViewModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;

        public double Latitude { get; set; }  
        public double Longitude { get; set; }   
    }
}