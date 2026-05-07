using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gosarajevovol3.Models;

public class SmartPlanner
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public DateTime ArrivalDate { get; set; } = DateTime.UtcNow;

    [Required]
    public DateTime DepartureDate { get; set; } = DateTime.UtcNow.AddDays(1);
    
    public int RegisteredUserId { get; set; }
    
    [ForeignKey("RegisteredUserId")] 
    public RegisteredUser RegisteredUser { get; set; } = null!;
    
    public int PreferenceId { get; set; }
    
    [ForeignKey("PreferenceId")] 
    public Preference Preference { get; set; } = null!;
    
    public List<Attraction> Attractions { get; set; } = new List<Attraction>();
    public List<Hospitality> HospitalitySites { get; set; } = new List<Hospitality>();
    public List<Event> Events { get; set; } = new List<Event>();
}