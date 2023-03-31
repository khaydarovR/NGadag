using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RRshop.Models;
using RRshop.ViewModels;

namespace RRshop.Controllers
{
    public class ProdsController : Controller
    {
        private readonly rrshopContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public ProdsController(rrshopContext context, IMapper mapping, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapping;
            _env = env;
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
            ViewData["CategoryTitle"] = new SelectList(_context.Categories, "Id", "Title");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateProdViewModel viewModel)
        {
            Prod? newProd = _mapper.Map<Prod>(viewModel);

            if (ModelState.IsValid)
            {
                _context.Add(newProd);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryTitle"] = new SelectList(_context.Categories, "Id", "Title");
            return View(viewModel);
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
            ViewData["CategoryTitle"] = new SelectList(_context.Categories, "Id", "Title");

            var viewModel = _mapper.Map<EditProdViewModel>(prod);
            return View(viewModel);
        }


        [HttpPost]
        public IActionResult Edit(int id, EditProdViewModel viewModel)
        {

            var prod = _mapper.Map<Prod>(viewModel);

            if (viewModel.IsDefaultImage) { ResetImage(viewModel.Id); }

            if (id != prod.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {

                    string? title = prod.Title;
                    string? color = prod.Color;
                    _context.Database.ExecuteSqlInterpolated(@$"UPDATE prod SET title = {title},
                                                                category_id = {prod.CategoryId},
                                                                price = {prod.Price},
                                                                color = {color},
                                                                sale_quantity = {prod.SaleQuantity}
                                                                WHERE id = {id}");
                    _context.SaveChanges();
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
            ViewData["CategoryTitle"] = new SelectList(_context.Categories, "Id", "Title");
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
                DirectoryInfo df = new DirectoryInfo(_env.WebRootPath + "/img/prod/" + prod.ImgPath);

                _context.Prods.Remove(prod);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdExists(int id)
        {
            return (_context.Prods?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private async Task<string> SaveImage(ImageModel imageModel)
        {
            if (imageModel != null)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(_env.WebRootPath + "/img/prod");
                if (directoryInfo.Exists) { directoryInfo.Create(); }
            }

            string fileName = Path.GetFileNameWithoutExtension(imageModel.ImageFile.FileName);
            string extension = Path.GetExtension(imageModel.ImageFile.FileName).ToLower();

            string fulFilename = imageModel.ProdId + fileName + extension;
            string fullPath = Path.Combine(_env.WebRootPath + "/img/prod/" + fulFilename);

            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                await imageModel.ImageFile.CopyToAsync(fileStream);
            }

            return fulFilename;
        }


        [HttpGet]
        public async Task<IActionResult> AddImage(int id)
        {
            ImageModel image = new ImageModel() { ProdId = id };
            return View(image);

        }


        [HttpPost]
        public async Task<IActionResult> AddImage(ImageModel imageModel)
        {
            if (ModelState.IsValid)
            {
                string fulFilename = await SaveImage(imageModel);

                var dbProd = await _context.Prods.FirstAsync(db => db.Id == imageModel.ProdId);
                dbProd.ImgPath = fulFilename;
                _context.Update(dbProd);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(imageModel);
        }


        private void ResetImage(int id)
        {
            var prod = _context.Prods.First(db => db.Id == id);
            prod.ImgPath = "default.png";
            _context.SaveChanges();
        }
    }
}
