using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using gosarajevovol3.Data;
using gosarajevovol3.Models;
using gosarajevovol3.Models.Enums;

namespace gosarajevovol3.Controllers
{
    public class ThingsToDoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ThingsToDoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Glavna stranica sa kategorijama
        public async Task<IActionResult> Index()
        {
            var restaurants = await _context.Hospitality
                .Where(h => h.HospitalityType == HospitalityType.Restaurant)
                .ToListAsync();
            var cafes = await _context.Hospitality
                .Where(h => h.HospitalityType == HospitalityType.Cafe)
                .ToListAsync();
            var bars = await _context.Hospitality
                .Where(h => h.HospitalityType == HospitalityType.Bars)
                .ToListAsync();
            var shopping = await _context.Hospitality
                .Where(h => h.HospitalityType == HospitalityType.Shopping)
                .ToListAsync();
            var cinemas = await _context.Hospitality
                .Where(h => h.HospitalityType == HospitalityType.Cinemas)
                .ToListAsync();
            var museums = await _context.Hospitality
                .Where(h => h.HospitalityType == HospitalityType.Museums)
                .ToListAsync();

            ViewBag.Restaurants = restaurants;
            ViewBag.Cafes = cafes;
            ViewBag.Bars = bars;
            ViewBag.Shopping = shopping;
            ViewBag.Cinemas = cinemas;
            ViewBag.Museums = museums;

            return View();
        }

        // Lista Hospitality objekata po tipu (Restaurants, Cafes, Bars)
        public async Task<IActionResult> Hospitality(HospitalityType type)
        {
            var items = await _context.Hospitality
                .Where(h => h.HospitalityType == type)
                .ToListAsync();

            ViewBag.CategoryName = type.ToString();
            return View(items);
        }

        // Detalji Hospitality objekta + recenzije
        public async Task<IActionResult> HospitalityDetails(int id)
        {
            var item = await _context.Hospitality
                .Include(h => h.Reviews)
                    .ThenInclude(r => r.RegisteredUser)
                .FirstOrDefaultAsync(h => h.Id == id);

            if (item == null) return NotFound();

            return View(item);
        }
    }
}