using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RRshop.Models;

namespace RRshop.Controllers
{
    public class ProdsController : Controller
    {
        private readonly rrshopContext _context;

        public ProdsController(rrshopContext context)
        {
            _context = context;
        }

        // GET: Prods
        public async Task<IActionResult> Index()
        {
            var rrshopContext = _context.Prods.Include(p => p.Category);
            return View(await rrshopContext.ToListAsync());
        }

        // GET: Prods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Prods == null)
            {
                return NotFound();
            }

            var prod = await _context.Prods
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prod == null)
            {
                return NotFound();
            }

            return View(prod);
        }

        // GET: Prods/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id");
            return View();
        }

        // POST: Prods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Title,CategoryId,Price,Color,SaleQuantity,ImgPath")] Prod prod)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prod);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", prod.CategoryId);
            return View(prod);
        }

        // GET: Prods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Prods == null)
            {
                return NotFound();
            }

            var prod = await _context.Prods.FindAsync(id);
            if (prod == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", prod.CategoryId);
            return View(prod);
        }

        // POST: Prods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,CategoryId,Price,Color,SaleQuantity,ImgPath")] Prod prod)
        {
            if (id != prod.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prod);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdExists(prod.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", prod.CategoryId);
            return View(prod);
        }

        // GET: Prods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Prods == null)
            {
                return NotFound();
            }

            var prod = await _context.Prods
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prod == null)
            {
                return NotFound();
            }

            return View(prod);
        }

        // POST: Prods/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Prods == null)
            {
                return Problem("Entity set 'rrshopContext.Prods'  is null.");
            }
            var prod = await _context.Prods.FindAsync(id);
            if (prod != null)
            {
                _context.Prods.Remove(prod);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdExists(int id)
        {
          return (_context.Prods?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
