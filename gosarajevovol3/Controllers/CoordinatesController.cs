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
    public class CoordinatesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoordinatesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Coordinates
        public async Task<IActionResult> Index()
        {
            return View(await _context.Coordinates.ToListAsync());
        }

        // GET: Coordinates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coordinates = await _context.Coordinates
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coordinates == null)
            {
                return NotFound();
            }

            return View(coordinates);
        }

        // GET: Coordinates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Coordinates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Latitude,Longitude")] Coordinates coordinates)
        {
            if (ModelState.IsValid)
            {
                _context.Add(coordinates);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(coordinates);
        }

        // GET: Coordinates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coordinates = await _context.Coordinates.FindAsync(id);
            if (coordinates == null)
            {
                return NotFound();
            }
            return View(coordinates);
        }

        // POST: Coordinates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Latitude,Longitude")] Coordinates coordinates)
        {
            if (id != coordinates.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coordinates);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoordinatesExists(coordinates.Id))
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
            return View(coordinates);
        }

        // GET: Coordinates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coordinates = await _context.Coordinates
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coordinates == null)
            {
                return NotFound();
            }

            return View(coordinates);
        }

        // POST: Coordinates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var coordinates = await _context.Coordinates.FindAsync(id);
            if (coordinates != null)
            {
                _context.Coordinates.Remove(coordinates);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CoordinatesExists(int id)
        {
            return _context.Coordinates.Any(e => e.Id == id);
        }
    }
}
