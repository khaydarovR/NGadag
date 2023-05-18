using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NGadag.Models;
using NGadag.ViewModels;
using NGadag.Data;
using RRshop.ViewModels;
using System;
using NGadag.DTO;
using System.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NGadag.Controllers
{
    public class AdsController : Controller
    {
        private readonly ngadagContext _context;
        private readonly IWebHostEnvironment env;
        private readonly IMapper mapping;

        public AdsController(ngadagContext context, IWebHostEnvironment env, IMapper mapping)
        {
            _context = context;
            this.env = env;
            this.mapping = mapping;
        }

        // GET: Ads
        public async Task<IActionResult> Index()
        {
            var res = await _context.Ads.ToListAsync();
            foreach(var m in res)
            {
                m.Descriptions = m.Descriptions is not null ? Utils.Substring(m.Descriptions, 50) : "null";
            }
            return View(res);
        }

        // GET: Ads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ads == null)
            {
                return NotFound();
            }

            var ad = await _context.Ads
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ad == null)
            {
                return NotFound();
            }

            return View(ad);
        }

        // GET: Ads/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateAdViewModel model)
        {
           
            if (ModelState.IsValid)
            {
                string fulFilename = await SaveImage(model);
                Ad newAd = mapping.Map<Ad>(model);
                newAd.Photo = fulFilename;

                await _context.Ads.AddAsync(newAd);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        private async Task<string> SaveImage(CreateAdViewModel model)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(env.WebRootPath + Literals.PathForProdImg);
            if (directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
            
            string fileName = Path.GetFileNameWithoutExtension(model.ImageFile.Length.ToString());
            string extension = Path.GetExtension(model.ImageFile.FileName).ToLower();

            string fulFilename = DateTime.Now.Hour.ToString()
                + DateTime.Now.DayOfYear.ToString()
                + DateTime.Now.Minute.ToString()
                + DateTime.Now.Second.ToString()
                + fileName + extension;
            string fullPath = Path.Combine(env.WebRootPath + Literals.PathForProdImg + fulFilename);

            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                await model.ImageFile.CopyToAsync(fileStream);
            }

            return fulFilename;
        }

        private void DeleteOldImage(string imageTitle)
        {
            if (imageTitle != Literals.DefaultProdImgName)
            {
                FileInfo file = new FileInfo(Path.Combine(env.WebRootPath + Literals.PathForProdImg + imageTitle));
                file.Delete();
            }
        }

        private void ResetImage(int id)
        {
            var prod = _context.Ads.First(db => db.Id == id);
            if (prod.Photo != Literals.DefaultProdImgName)
            {
                FileInfo file = new FileInfo(Path.Combine(env.WebRootPath + Literals.PathForProdImg + prod));
                file.Delete();
                prod.Photo = Literals.DefaultProdImgName;
                _context.Update(prod);
                _context.SaveChanges();
            }
        }

        // GET: Ads/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ads == null)
            {
                return NotFound();
            }

            var ad = await _context.Ads.FindAsync(id);
            if (ad == null)
            {
                return NotFound();
            }
            return View(ad);
        }

        // POST: Ads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Descriptions,Icon,Photo")] Ad ad)
        {
            if (id != ad.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdExists(ad.Id))
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
            return View(ad);
        }

        // GET: Ads/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ads == null)
            {
                return NotFound();
            }

            var ad = await _context.Ads
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ad == null)
            {
                return NotFound();
            }

            return View(ad);
        }

        // POST: Ads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ads == null)
            {
                return Problem("Entity set 'ngadagContext.Ads'  is null.");
            }
            var ad = await _context.Ads.FindAsync(id);
            if (ad != null)
            {
                _context.Ads.Remove(ad);
                DeleteOldImage(ad.Photo.ToString());
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdExists(int id)
        {
          return (_context.Ads?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
