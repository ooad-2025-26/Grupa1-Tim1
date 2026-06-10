using gosarajevovol3.Data;
using gosarajevovol3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using SendGrid; 
using SendGrid.Helpers.Mail; 

namespace gosarajevovol3.Controllers;

[Authorize]
[ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
public class OperatorController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration; 

    public OperatorController(ApplicationDbContext context, UserManager<User> userManager, IConfiguration configuration)
    {
        _context = context;
        _userManager = userManager;
        _configuration = configuration; 
    }
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var currentUser = await _userManager.GetUserAsync(User);
        if (currentUser is not Operator)
        {
            context.Result = new ForbidResult();
            return;
        }
        await next();
    }
    public async Task<IActionResult> Index()
    {
        var sviKorisnici = await _context.RegisteredUsers.ToListAsync();

        ViewBag.TotalSubscribers = sviKorisnici.Count;

        return View(sviKorisnici);
    }
    [HttpGet]
    public IActionResult CreateNewsletter()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SendNewsletter(string subject, string messageContent)
    {
        if (string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(messageContent))
        {
            ModelState.AddModelError("", "Subject and Content cannot be empty.");
            return View("CreateNewsletter");
        }
        var sviEmailovi = await _context.RegisteredUsers
            .Select(u => u.Email)
            .Where(email => !string.IsNullOrEmpty(email)) 
            .ToListAsync();

        if (!sviEmailovi.Any())
        {
            TempData["SuccessMessage"] = "No registered users found.";
            return RedirectToAction("Index");
        }
        var apiKey = _configuration["SendGridSettings:ApiKey"];
        var fromEmail = _configuration["SendGridSettings:FromEmail"];
        var fromName = _configuration["SendGridSettings:FromName"];
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress(fromEmail, fromName);
        
        var msg = new SendGridMessage()
        {
            From = from,
            Subject = subject,
            PlainTextContent = messageContent,
            HtmlContent = messageContent 
        };
        msg.AddTo(new EmailAddress(fromEmail, fromName));
        foreach (var email in sviEmailovi)
        {
            if (email.Equals(fromEmail, StringComparison.OrdinalIgnoreCase)) continue;
    
            msg.AddBcc(new EmailAddress(email));
        }

        try
        {
            var response = await client.SendEmailAsync(msg);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = $"Newsletter successfully sent to all {sviEmailovi.Count} registered users!";
            }
            else
            {
                var errorBody = await response.Body.ReadAsStringAsync();
                ModelState.AddModelError("", $"API Error (Status: {response.StatusCode}): {errorBody}");
                return View("CreateNewsletter");
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"An error occurred while connecting: {ex.Message}");
            return View("CreateNewsletter");
        }
        
        return RedirectToAction("Index");
    }
}