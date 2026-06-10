using System.ComponentModel.DataAnnotations;

namespace gosarajevovol3.ViewModels;

public class LogInViewModel
{
    [Required(ErrorMessage = "Email is mandatory.")]
    [EmailAddress(ErrorMessage = "Format of Email is invalid.")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Password is mandatory.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    public bool RememberMe { get; set; }
}