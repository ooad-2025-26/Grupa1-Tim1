using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gosarajevovol3.Models;

public class Preference
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Range(1, 5, ErrorMessage = "Level must be between 1 and 5.")]
    public int History { get; set; }

    [Required]
    [Range(1, 5, ErrorMessage = "Level must be between 1 and 5.")]
    public int TraditionalFood { get; set; }

    [Required]
    [Range(1, 5, ErrorMessage = "Level must be between 1 and 5.")]
    public int Nature { get; set; }

    [Required]
    [Range(1, 5, ErrorMessage = "Level must be between 1 and 5.")]
    public int Culture { get; set; }

    [Required]
    [Range(1, 5, ErrorMessage = "Level must be between 1 and 5.")]
    public int Nightlife { get; set; }
    
    [ForeignKey("RegisteredUser")]
    public int RegisteredUserId { get; set; }
    public RegisteredUser RegisteredUser { get; set; } = null!;
}