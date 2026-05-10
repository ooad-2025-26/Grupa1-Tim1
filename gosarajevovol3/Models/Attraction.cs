using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using gosarajevovol2.Models.Enums;

namespace gosarajevovol3.Models;

public class Attraction
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(200)]
    public string AttractionName { get; set; } = string.Empty;
    
    [Required]
    [StringLength(300)]
    public string AttractionDescription { get; set; } = string.Empty;
    
    [StringLength(200)]
    public string PhotoUrl { get; set; } = string.Empty;
    
    [Required]
    public AttractionType AttractionType { get; set; }
    
    [ForeignKey("Coordinates")]
    public int CoordinatesId { get; set; }
    public Coordinates Coordinates { get; set; } = new Coordinates();
    
    [Required]
    [StringLength(250)]
    public string LocationAddress { get; set; } = string.Empty;
}