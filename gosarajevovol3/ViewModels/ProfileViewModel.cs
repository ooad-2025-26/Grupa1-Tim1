using gosarajevovol3.Models;

namespace gosarajevovol3.ViewModels;

public class ProfileViewModel
{
    public string Email { get; set; } = string.Empty;
    public List<Review> Reviews { get; set; } = new();
    public List<SmartPlanner> Plans { get; set; } = new();
}