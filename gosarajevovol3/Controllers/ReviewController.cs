using gosarajevovol3.Data;
using gosarajevovol3.Models;
using gosarajevovol3.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace gosarajevovol3.Controllers;

public class ReviewController : Controller
{
    private readonly ApplicationDbContext _context;

    public ReviewController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Leave(int hospitalityId)
    {
        if (User.Identity?.IsAuthenticated != true)
            return RedirectToAction("Login", "Account");

        var user = await _context.RegisteredUsers
            .FirstOrDefaultAsync(u => u.Email == User.Identity.Name);
        if (user == null)
            return RedirectToAction("Login", "Account");

        var hospitality = await _context.Hospitality
            .FirstOrDefaultAsync(h => h.Id == hospitalityId);
        if (hospitality == null) return NotFound();

        var existing = await _context.Reviews
            .FirstOrDefaultAsync(r => r.HospitalityId == hospitalityId
                                   && r.RegisteredUserId == user.Id);

        var vm = new ReviewFormViewModel
        {
            ReviewId = existing?.Id,
            HospitalityId = hospitalityId,
            HospitalityName = hospitality.HospitalityName,
            Rating = existing?.Rating ?? 0,
            Comment = existing?.Comment ?? ""
        };

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Leave(ReviewFormViewModel model)
    {
        if (User.Identity?.IsAuthenticated != true)
            return RedirectToAction("Login", "Account");

        var user = await _context.RegisteredUsers
            .FirstOrDefaultAsync(u => u.Email == User.Identity.Name);
        if (user == null)
            return RedirectToAction("Login", "Account");

        if (model.Rating < 1 || model.Rating > 5)
            ModelState.AddModelError(nameof(model.Rating), "Please select a rating (1-5 stars).");
        if (string.IsNullOrWhiteSpace(model.Comment))
            ModelState.AddModelError(nameof(model.Comment), "Please write a comment.");

        if (!ModelState.IsValid)
        {
            var h = await _context.Hospitality
                .FirstOrDefaultAsync(x => x.Id == model.HospitalityId);
            model.HospitalityName = h?.HospitalityName ?? "";
            return View(model);
        }

        if (model.ReviewId.HasValue)
        {
            var review = await _context.Reviews
                .FirstOrDefaultAsync(r => r.Id == model.ReviewId.Value
                                       && r.RegisteredUserId == user.Id);
            if (review == null) return NotFound();

            review.Rating = model.Rating;
            review.Comment = model.Comment;
            _context.Reviews.Update(review);
        }
        else
        {
            _context.Reviews.Add(new Review
            {
                Rating = model.Rating,
                Comment = model.Comment,
                CreatedAt = DateTime.UtcNow,
                RegisteredUserId = user.Id,
                HospitalityId = model.HospitalityId
            });
        }

        await _context.SaveChangesAsync();

        return RedirectToAction("HospitalityDetails", "ThingsToDo",
            new { id = model.HospitalityId });
    }

    // POST: brisanje vlastite recenzije
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, int hospitalityId = 0)
    {
        if (User.Identity?.IsAuthenticated != true)
            return RedirectToAction("Login", "Account");

        var user = await _context.RegisteredUsers
            .FirstOrDefaultAsync(u => u.Email == User.Identity.Name);
        if (user == null)
            return RedirectToAction("Login", "Account");

        var review = await _context.Reviews
            .FirstOrDefaultAsync(r => r.Id == id && r.RegisteredUserId == user.Id);
        if (review == null) return NotFound();

        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync();

        // ako je brisano sa details stranice -> vrati tamo, inace na profil
        if (hospitalityId > 0)
            return RedirectToAction("HospitalityDetails", "ThingsToDo", new { id = hospitalityId });

        return RedirectToAction("Profile", "UserProfile");
    }
}