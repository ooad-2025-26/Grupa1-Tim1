using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using gosarajevovol2.Models.Enums;

namespace gosarajevovol3.Models;

public class Event
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(200)]
    public string EventName { get; set; } = string.Empty;
    
    [Required]
    public string EventDescription { get; set; } = string.Empty;
    
    [Required]
    public DateTime StartDate { get; set; }
    
    [Required]
    public DateTime EndDate { get; set; }
    
    [Required]
    public EventType Type { get; set; }
    
    public string PhotoUrl { get; set; } = string.Empty;
    public string WebUrl { get; set; } = string.Empty;
    
    [ForeignKey("Coordinates")]
    public int CoordinatesId { get; set; }
    public Coordinates Coordinates { get; set; } = new Coordinates();
    
    [Required]
    [StringLength(250)]
    public string LocationAddress { get; set; } = string.Empty; 
}