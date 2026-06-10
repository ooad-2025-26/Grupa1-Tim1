using System.ComponentModel.DataAnnotations;

namespace gosarajevovol3.ViewModels;

public class ForgotPasswordViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}