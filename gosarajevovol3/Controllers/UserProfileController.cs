using gosarajevovol3.Data;
using gosarajevovol3.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace gosarajevovol3.Controllers;

public class UserProfileController : Controller
{
    private readonly ApplicationDbContext _context;

    public UserProfileController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        if (User.Identity?.IsAuthenticated != true)
            return RedirectToAction("Login", "Account");

        var email = User.Identity.Name;

        var user = await _context.RegisteredUsers
            .FirstOrDefaultAsync(x => x.Email == email);

        if (user == null)
            return RedirectToAction("Login", "Account");

        var reviews = await _context.Reviews
            .Include(r => r.Hospitality)
            .Where(r => r.RegisteredUserId == user.Id)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();

        var plans = await _context.SmartPlanners
            .Where(p => p.RegisteredUserId == user.Id)
            .OrderBy(p => p.Id)
            .ToListAsync();

        var model = new ProfileViewModel
        {
            Email = user.Email ?? "",
            Reviews = reviews,
            Plans = plans
        };

        return View(model);
    }
}