using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using gosarajevovol3.Data;
using gosarajevovol3.Models;

namespace gosarajevovol3.Controllers
{
    public class AttracionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AttracionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Attracion
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Attractions.Include(a => a.Coordinates);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Attracion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attraction = await _context.Attractions
                .Include(a => a.Coordinates)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attraction == null)
            {
                return NotFound();
            }

            return View(attraction);
        }

        // GET: Attracion/Create
        public IActionResult Create()
        {
            ViewData["CoordinatesId"] = new SelectList(_context.Coordinates, "Id", "Id");
            return View();
        }

        // POST: Attracion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AttractionName,AttractionDescription,PhotoUrl,AttractionType,CoordinatesId,LocationAddress")] Attraction attraction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(attraction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CoordinatesId"] = new SelectList(_context.Coordinates, "Id", "Id", attraction.CoordinatesId);
            return View(attraction);
        }

        // GET: Attracion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attraction = await _context.Attractions.FindAsync(id);
            if (attraction == null)
            {
                return NotFound();
            }
            ViewData["CoordinatesId"] = new SelectList(_context.Coordinates, "Id", "Id", attraction.CoordinatesId);
            return View(attraction);
        }

        // POST: Attracion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AttractionName,AttractionDescription,PhotoUrl,AttractionType,CoordinatesId,LocationAddress")] Attraction attraction)
        {
            if (id != attraction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attraction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttractionExists(attraction.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CoordinatesId"] = new SelectList(_context.Coordinates, "Id", "Id", attraction.CoordinatesId);
            return View(attraction);
        }

        // GET: Attracion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attraction = await _context.Attractions
                .Include(a => a.Coordinates)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attraction == null)
            {
                return NotFound();
            }

            return View(attraction);
        }

        // POST: Attracion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var attraction = await _context.Attractions.FindAsync(id);
            if (attraction != null)
            {
                _context.Attractions.Remove(attraction);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttractionExists(int id)
        {
            return _context.Attractions.Any(e => e.Id == id);
        }
    }
}
