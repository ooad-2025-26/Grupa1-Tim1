using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gosarajevovol3.Models;

public class Review
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Comment { get; set; } = string.Empty;
    
    [Required]
    [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5 stars.")]
    public int Rating { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [ForeignKey("RegisteredUser")]
    public int RegisteredUserId { get; set; }
    public RegisteredUser RegisteredUser { get; set; } = null!;
    
    [ForeignKey("Hospitality")]
    public int? HospitalityId { get; set; }
    public Hospitality? Hospitality { get; set; }
}