using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using gosarajevovol2.Models.Enums;

namespace gosarajevovol3.Models;

public class Newsletter
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Subject { get; set; } = string.Empty;
    
    [Required]
    public string Content { get; set; } = string.Empty;
    
    [Required]
    public DateTime SentAt { get; set; } = DateTime.UtcNow;
    
    [Required]
    public NewsletterStatus Status { get; set; } = NewsletterStatus.Draft;
    
    [ForeignKey("Operator")]
    public int OperatorId { get; set; }
    public Operator Operator { get; set; } = null!;
}