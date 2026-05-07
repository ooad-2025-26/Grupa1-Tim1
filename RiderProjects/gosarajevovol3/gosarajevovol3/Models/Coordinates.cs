using System.ComponentModel.DataAnnotations;

namespace gosarajevovol3.Models;

public class Coordinates
{
    [Key]
    public int Id { get; set; }
    
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}