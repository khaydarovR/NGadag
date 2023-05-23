using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NGadag.Data;
using NGadag.Models;
using NGadag.ViewModels;

namespace NGadag.Controllers
{
    [Authorize(Roles = Roles.Root)]
    public class ApplicationsController : Controller
    {
        private readonly ngadagContext _context;

        public ApplicationsController(ngadagContext context)
        {
            _context = context;
        }

        // GET: Applications
        public async Task<IActionResult> Index()
        {
            return _context.Applications != null ?
                        View(await _context.Applications.ToListAsync()) :
                        Problem("Entity set 'ngadag.Applications'  is null.");
        }

        // GET: Applications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Applications == null)
            {
                return NotFound();
            }

            var Applications = await _context.Applications
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Applications == null)
            {
                return NotFound();
            }

            return View(Applications);
        }

        // GET: Applications/Create
        [AllowAnonymous]
        public IActionResult Create()
        {
            var compInfo = _context.Companyinfos.ToList();
            var model = new CreateInfo()
            {
                CompInfo = compInfo,
                Applications = new Applications()
            };
            return View(model);
        }


        // POST: Applications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Phone,Message")] Applications Applications)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Applications);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(Applications);
        }

        // GET: Applications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Applications == null)
            {
                return NotFound();
            }

            var Applications = await _context.Applications.FindAsync(id);
            if (Applications == null)
            {
                return NotFound();
            }
            return View(Applications);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Phone,Message")] Applications Applications)
        {
            if (id != Applications.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Applications);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationsExists(Applications.Id))
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
            return View(Applications);
        }

        // GET: Applications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Applications == null)
            {
                return NotFound();
            }

            var Applications = await _context.Applications
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Applications == null)
            {
                return NotFound();
            }

            return View(Applications);
        }

        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Applications == null)
            {
                return Problem("Entity set 'ngadag.Applications'  is null.");
            }
            var Applications = await _context.Applications.FindAsync(id);
            if (Applications != null)
            {
                _context.Applications.Remove(Applications);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationsExists(int id)
        {
            return (_context.Applications?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
