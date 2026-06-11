using gosarajevovol3.Data;
using gosarajevovol3.Models;
using gosarajevovol3.Services.Abstract;
using gosarajevovol3.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SendGrid; 
using SendGrid.Helpers.Mail; 

namespace gosarajevovol3.Controllers;

public class AccountController : Controller
{
    private readonly IUserService _userService;
    private readonly UserManager<User> _userManager;
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration; 
    public AccountController(
        IUserService userService,
        UserManager<User> userManager,
        ApplicationDbContext context,
        IConfiguration configuration)
    {
        _userService = userService;
        _userManager = userManager;
        _context = context;
        _configuration = configuration; 
    }

    
    [HttpGet]
    public IActionResult Login()
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("ProfileRoute");
        }
        return View();
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LogInViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result = await _userService.LoginAsync(
            model.Email,
            model.Password,
            rememberMe: false
        );

        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Invalid email or password");
            return View(model);
        }

        var admin = await _context.Admins
            .FirstOrDefaultAsync(x => x.Email == model.Email);

        if (admin != null)
        {
            return RedirectToAction("Dashboard", "Admin");
        }

        var oper = await _context.Operators
            .FirstOrDefaultAsync(x => x.Email == model.Email);

        if (oper != null)
        {
            return RedirectToAction("Index", "Operator");
        }

        var user = await _context.RegisteredUsers
            .FirstOrDefaultAsync(x => x.Email == model.Email);

        if (user != null)
        {
            return RedirectToAction("Profile", "UserProfile");
        }

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _userService.LogoutAsync(); 
        return RedirectToAction("Login", "Account");
    }

    [HttpGet]
    public async Task<IActionResult> ProfileRoute()
    {
        if (User.Identity?.IsAuthenticated != true)
        {
            return RedirectToAction("Login", "Account");
        }

        var userEmail = User.Identity.Name; 
        if (string.IsNullOrEmpty(userEmail))
        {
            return RedirectToAction("Login", "Account");
        }

        var isAdmin = await _context.Admins.AnyAsync(x => x.Email == userEmail);
        if (isAdmin)
        {
            return RedirectToAction("Dashboard", "Admin");
        }

        var isOperator = await _context.Operators.AnyAsync(x => x.Email == userEmail);
        if (isOperator)
        {
            return RedirectToAction("Index", "Operator");
        }

        var isRegisteredUser = await _context.RegisteredUsers.AnyAsync(x => x.Email == userEmail);
        if (isRegisteredUser)
        {
            return RedirectToAction("Profile", "UserProfile");
        }

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View(); 
    }

    [HttpGet]
    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = await _userManager.FindByEmailAsync(model.Email);
        
        if (user != null)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var resetLink = Url.Action("ResetPassword", "Account", 
                new { token = token, email = model.Email }, Request.Scheme);

            var apiKey = _configuration["SendGridSettings:ApiKey"];
            var fromEmail = _configuration["SendGridSettings:FromEmail"];
            var fromName = _configuration["SendGridSettings:FromName"];

            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(fromEmail, fromName);
            var to = new EmailAddress(model.Email);
            
            var subject = "GoSarajevo - Reset Your Password";
            var htmlContent = $@"
                <div style='font-family: Arial, sans-serif; padding: 20px; max-width: 600px; margin: auto; border: 1px solid #eee; border-radius: 10px;'>
                    <h2 style='color: #0c3156;'>Password Reset Request</h2>
                    <p>We received a request to reset the password for your GoSarajevo account.</p>
                    <p>Click the button below to choose a new password:</p>
                    <p style='text-align: center; margin: 30px 0;'>
                        <a href='{resetLink}' style='background-color: #05335e; color: white; padding: 12px 25px; text-decoration: none; border-radius: 8px; font-weight: bold; display: inline-block;'>Reset Password</a>
                    </p>
                    <p style='color: #666; font-size: 0.9rem;'>If you did not request this change, you can safely ignore this email.</p>
                </div>";

            var msg = new SendGridMessage()
            {
                From = from,
                Subject = subject,
                HtmlContent = htmlContent,
                PlainTextContent = "Please use an HTML capable email client to reset your password." 
            };
            msg.AddTo(to);

            try
            {
                await client.SendEmailAsync(msg);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Failed to send email: {ex.Message}");
                return View(model);
            }
        }

        TempData["SuccessMessage"] = "A reset link has been sent.";
        return View();
    }

    [HttpGet]
    public IActionResult ResetPassword(string token, string email)
    {
        if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(email))
        {
            return RedirectToAction("Login");
        }

        var model = new ResetPasswordViewModel { Token = token, Email = email };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            return RedirectToAction("ResetPasswordConfirmation");
        }

        var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
        
        if (result.Succeeded)
        {
            return RedirectToAction("ResetPasswordConfirmation");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError("", error.Description);
        }

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> SwitchAccount()
    {
        await _userService.LogoutAsync();
        return RedirectToAction("Login", "Account");
    }

    [HttpGet]
    public IActionResult ResetPasswordConfirmation()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var noviKorisnik = new RegisteredUser
        {
            UserName = model.Email,
            Email = model.Email,
            NormalizedUserName = model.Email.ToUpper(), 
            NormalizedEmail = model.Email.ToUpper(),
            EmailConfirmed = true 
        };
        var result = await _userManager.CreateAsync(noviKorisnik, model.Password);

        if (result.Succeeded)
        {
            await _userService.LoginAsync(model.Email, model.Password, rememberMe: false);
        
            return RedirectToAction("ProfileRoute");
        }
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError("", error.Description);
        }

        return View(model);
    }
}