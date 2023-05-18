using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NGadag.Models;

namespace NGadag.Controllers
{
    public class CompanyinfoesController : Controller
    {
        private readonly ngadagContext _context;

        public CompanyinfoesController(ngadagContext context)
        {
            _context = context;
        }

        // GET: Companyinfoes
        public async Task<IActionResult> Index()
        {
              return _context.Companyinfos != null ? 
                          View(await _context.Companyinfos.ToListAsync()) :
                          Problem("Entity set 'ngadagContext.Companyinfos'  is null.");
        }

        // GET: Companyinfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Companyinfos == null)
            {
                return NotFound();
            }

            var companyinfo = await _context.Companyinfos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyinfo == null)
            {
                return NotFound();
            }

            return View(companyinfo);
        }

        // GET: Companyinfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Companyinfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,InfoType,InfoValue")] Companyinfo companyinfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(companyinfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(companyinfo);
        }

        // GET: Companyinfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Companyinfos == null)
            {
                return NotFound();
            }

            var companyinfo = await _context.Companyinfos.FindAsync(id);
            if (companyinfo == null)
            {
                return NotFound();
            }
            return View(companyinfo);
        }

        // POST: Companyinfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,InfoType,InfoValue")] Companyinfo companyinfo)
        {
            if (id != companyinfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(companyinfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyinfoExists(companyinfo.Id))
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
            return View(companyinfo);
        }

        // GET: Companyinfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Companyinfos == null)
            {
                return NotFound();
            }

            var companyinfo = await _context.Companyinfos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyinfo == null)
            {
                return NotFound();
            }

            return View(companyinfo);
        }

        // POST: Companyinfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Companyinfos == null)
            {
                return Problem("Entity set 'ngadagContext.Companyinfos'  is null.");
            }
            var companyinfo = await _context.Companyinfos.FindAsync(id);
            if (companyinfo != null)
            {
                _context.Companyinfos.Remove(companyinfo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyinfoExists(int id)
        {
          return (_context.Companyinfos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
