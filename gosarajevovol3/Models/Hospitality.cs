using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using gosarajevovol2.Models.Enums;

namespace gosarajevovol3.Models;

public class Hospitality
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(200)]
    public string HospitalityName { get; set; } = string.Empty;
    
    [Required]
    public string HospitalityDescription { get; set; } = string.Empty;
    
    [StringLength(250)]
    public string PhotoUrl { get; set; } = string.Empty;
    
    [Required]
    public HospitalityType HospitalityType { get; set; }
    
    [Required]
    [StringLength(250)]
    public string LocationAddress { get; set; } = string.Empty;
    
    [ForeignKey("Coordinates")]
    public int CoordinatesId { get; set; }
    public Coordinates Coordinates { get; set; } = new Coordinates();
    
    [StringLength(250)]
    public string? GooglePlaceId { get; set; }
    
    public List<Review> Reviews { get; set; } = new List<Review>();
}