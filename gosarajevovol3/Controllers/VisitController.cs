using Microsoft.AspNetCore.Mvc;
using gosarajevovol3.Models;
using gosarajevovol2.Models.Enums;
using gosarajevovol3.Data; 
using Microsoft.EntityFrameworkCore;

namespace gosarajevovol3.Controllers
{
    public class VisitController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VisitController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string type = null)
        {
            var atrakcije = _context.Attractions
                .Include(a => a.Coordinates)
                .AsQueryable();

            if (!string.IsNullOrEmpty(type) && Enum.TryParse<AttractionType>(type, out var typeEnum))
                atrakcije = atrakcije.Where(a => a.AttractionType == typeEnum);

            ViewBag.ActiveType = type;
            return View(atrakcije.ToList());
        }

        public IActionResult Details(int id)
        {
            var atrakcija = _context.Attractions
                .Include(a => a.Coordinates)
                .FirstOrDefault(a => a.Id == id);

            if (atrakcija == null)
                return NotFound();

            return View(atrakcija);
        }
    }
}