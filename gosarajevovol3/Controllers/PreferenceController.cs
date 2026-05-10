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
    public class PreferenceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PreferenceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Preference
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Preferences.Include(p => p.RegisteredUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Preference/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preference = await _context.Preferences
                .Include(p => p.RegisteredUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (preference == null)
            {
                return NotFound();
            }

            return View(preference);
        }

        // GET: Preference/Create
        public IActionResult Create()
        {
            ViewData["RegisteredUserId"] = new SelectList(_context.RegisteredUsers, "Id", "Email");
            return View();
        }

        // POST: Preference/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,History,TraditionalFood,Nature,Culture,Nightlife,RegisteredUserId")] Preference preference)
        {
            if (ModelState.IsValid)
            {
                _context.Add(preference);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RegisteredUserId"] = new SelectList(_context.RegisteredUsers, "Id", "Email", preference.RegisteredUserId);
            return View(preference);
        }

        // GET: Preference/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preference = await _context.Preferences.FindAsync(id);
            if (preference == null)
            {
                return NotFound();
            }
            ViewData["RegisteredUserId"] = new SelectList(_context.RegisteredUsers, "Id", "Email", preference.RegisteredUserId);
            return View(preference);
        }

        // POST: Preference/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,History,TraditionalFood,Nature,Culture,Nightlife,RegisteredUserId")] Preference preference)
        {
            if (id != preference.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(preference);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PreferenceExists(preference.Id))
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
            ViewData["RegisteredUserId"] = new SelectList(_context.RegisteredUsers, "Id", "Email", preference.RegisteredUserId);
            return View(preference);
        }

        // GET: Preference/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preference = await _context.Preferences
                .Include(p => p.RegisteredUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (preference == null)
            {
                return NotFound();
            }

            return View(preference);
        }

        // POST: Preference/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var preference = await _context.Preferences.FindAsync(id);
            if (preference != null)
            {
                _context.Preferences.Remove(preference);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PreferenceExists(int id)
        {
            return _context.Preferences.Any(e => e.Id == id);
        }
    }
}
