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
    public class HospitalityController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HospitalityController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Hospitality
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Hospitality.Include(h => h.Coordinates);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Hospitality/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hospitality = await _context.Hospitality
                .Include(h => h.Coordinates)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hospitality == null)
            {
                return NotFound();
            }

            return View(hospitality);
        }

        // GET: Hospitality/Create
        public IActionResult Create()
        {
            ViewData["CoordinatesId"] = new SelectList(_context.Coordinates, "Id", "Id");
            return View();
        }

        // POST: Hospitality/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HospitalityName,HospitalityDescription,PhotoUrl,HospitalityType,LocationAddress,CoordinatesId,GooglePlaceId")] Hospitality hospitality)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hospitality);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CoordinatesId"] = new SelectList(_context.Coordinates, "Id", "Id", hospitality.CoordinatesId);
            return View(hospitality);
        }

        // GET: Hospitality/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hospitality = await _context.Hospitality.FindAsync(id);
            if (hospitality == null)
            {
                return NotFound();
            }
            ViewData["CoordinatesId"] = new SelectList(_context.Coordinates, "Id", "Id", hospitality.CoordinatesId);
            return View(hospitality);
        }

        // POST: Hospitality/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HospitalityName,HospitalityDescription,PhotoUrl,HospitalityType,LocationAddress,CoordinatesId,GooglePlaceId")] Hospitality hospitality)
        {
            if (id != hospitality.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hospitality);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HospitalityExists(hospitality.Id))
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
            ViewData["CoordinatesId"] = new SelectList(_context.Coordinates, "Id", "Id", hospitality.CoordinatesId);
            return View(hospitality);
        }

        // GET: Hospitality/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hospitality = await _context.Hospitality
                .Include(h => h.Coordinates)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hospitality == null)
            {
                return NotFound();
            }

            return View(hospitality);
        }

        // POST: Hospitality/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hospitality = await _context.Hospitality.FindAsync(id);
            if (hospitality != null)
            {
                _context.Hospitality.Remove(hospitality);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HospitalityExists(int id)
        {
            return _context.Hospitality.Any(e => e.Id == id);
        }
    }
}
