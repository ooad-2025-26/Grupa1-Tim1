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
    public class NewsletterController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NewsletterController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Newsletter
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Newsletters.Include(n => n.Operator);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Newsletter/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsletter = await _context.Newsletters
                .Include(n => n.Operator)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (newsletter == null)
            {
                return NotFound();
            }

            return View(newsletter);
        }

        // GET: Newsletter/Create
        public IActionResult Create()
        {
            ViewData["OperatorId"] = new SelectList(_context.Operators, "Id", "Email");
            return View();
        }

        // POST: Newsletter/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Subject,Content,SentAt,Status,OperatorId")] Newsletter newsletter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(newsletter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OperatorId"] = new SelectList(_context.Operators, "Id", "Email", newsletter.OperatorId);
            return View(newsletter);
        }

        // GET: Newsletter/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsletter = await _context.Newsletters.FindAsync(id);
            if (newsletter == null)
            {
                return NotFound();
            }
            ViewData["OperatorId"] = new SelectList(_context.Operators, "Id", "Email", newsletter.OperatorId);
            return View(newsletter);
        }

        // POST: Newsletter/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Subject,Content,SentAt,Status,OperatorId")] Newsletter newsletter)
        {
            if (id != newsletter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(newsletter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsletterExists(newsletter.Id))
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
            ViewData["OperatorId"] = new SelectList(_context.Operators, "Id", "Email", newsletter.OperatorId);
            return View(newsletter);
        }

        // GET: Newsletter/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsletter = await _context.Newsletters
                .Include(n => n.Operator)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (newsletter == null)
            {
                return NotFound();
            }

            return View(newsletter);
        }

        // POST: Newsletter/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var newsletter = await _context.Newsletters.FindAsync(id);
            if (newsletter != null)
            {
                _context.Newsletters.Remove(newsletter);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsletterExists(int id)
        {
            return _context.Newsletters.Any(e => e.Id == id);
        }
    }
}
