using gosarajevovol3.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace gosarajevovol3.Controllers;

public class EventsController : Controller
{
    private readonly ApplicationDbContext _context;

    public EventsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        DateTime trenutnoVrijeme = DateTime.Now;

        var upcomingEvents = await _context.Events
            .Where(e => e.StartDate >= trenutnoVrijeme)
            .OrderBy(e => e.StartDate)
            .ToListAsync();

        return View(upcomingEvents);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetEventsJson()
    {
        var eventi = await _context.Events.ToListAsync();
    
        var jsonPodaci = eventi.Select(e => new
        {
            id = e.Id,
            title = string.IsNullOrEmpty(e.EventNameEn) ? e.EventName : e.EventNameEn,
            start = e.StartDate.ToString("yyyy-MM-ddTHH:mm:ss"),
            end = e.EndDate.ToString("yyyy-MM-ddTHH:mm:ss"),
            url = e.WebUrl
        });

        return Json(jsonPodaci);
    }
}