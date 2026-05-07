namespace gosarajevovol3.Models;

public class RegisteredUser : User
{
    public List<SmartPlanner> PlannerList{ get; set; } = new List<SmartPlanner>();
    
    public List<Review> ReviewList { get; set; } = new List<Review>();
    public Preference? Preference { get; set; }
}