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
    public class SmartPlannerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SmartPlannerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SmartPlanner
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SmartPlanners.Include(s => s.Preference).Include(s => s.RegisteredUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SmartPlanner/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var smartPlanner = await _context.SmartPlanners
                .Include(s => s.Preference)
                .Include(s => s.RegisteredUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (smartPlanner == null)
            {
                return NotFound();
            }

            return View(smartPlanner);
        }

        // GET: SmartPlanner/Create
        public IActionResult Create()
        {
            ViewData["PreferenceId"] = new SelectList(_context.Preferences, "Id", "Id");
            ViewData["RegisteredUserId"] = new SelectList(_context.RegisteredUsers, "Id", "Email");
            return View();
        }

        // POST: SmartPlanner/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ArrivalDate,DepartureDate,RegisteredUserId,PreferenceId")] SmartPlanner smartPlanner)
        {
            if (ModelState.IsValid)
            {
                _context.Add(smartPlanner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PreferenceId"] = new SelectList(_context.Preferences, "Id", "Id", smartPlanner.PreferenceId);
            ViewData["RegisteredUserId"] = new SelectList(_context.RegisteredUsers, "Id", "Email", smartPlanner.RegisteredUserId);
            return View(smartPlanner);
        }

        // GET: SmartPlanner/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var smartPlanner = await _context.SmartPlanners.FindAsync(id);
            if (smartPlanner == null)
            {
                return NotFound();
            }
            ViewData["PreferenceId"] = new SelectList(_context.Preferences, "Id", "Id", smartPlanner.PreferenceId);
            ViewData["RegisteredUserId"] = new SelectList(_context.RegisteredUsers, "Id", "Email", smartPlanner.RegisteredUserId);
            return View(smartPlanner);
        }

        // POST: SmartPlanner/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ArrivalDate,DepartureDate,RegisteredUserId,PreferenceId")] SmartPlanner smartPlanner)
        {
            if (id != smartPlanner.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(smartPlanner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SmartPlannerExists(smartPlanner.Id))
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
            ViewData["PreferenceId"] = new SelectList(_context.Preferences, "Id", "Id", smartPlanner.PreferenceId);
            ViewData["RegisteredUserId"] = new SelectList(_context.RegisteredUsers, "Id", "Email", smartPlanner.RegisteredUserId);
            return View(smartPlanner);
        }

        // GET: SmartPlanner/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var smartPlanner = await _context.SmartPlanners
                .Include(s => s.Preference)
                .Include(s => s.RegisteredUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (smartPlanner == null)
            {
                return NotFound();
            }

            return View(smartPlanner);
        }

        // POST: SmartPlanner/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var smartPlanner = await _context.SmartPlanners.FindAsync(id);
            if (smartPlanner != null)
            {
                _context.SmartPlanners.Remove(smartPlanner);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SmartPlannerExists(int id)
        {
            return _context.SmartPlanners.Any(e => e.Id == id);
        }
    }
}
