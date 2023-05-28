using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NGadag.DTO;
using NGadag.Models;
using NGadag.ViewModels;
using System.Diagnostics;

namespace RRshop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ngadagContext context;

        public HomeController(ILogger<HomeController> logger, ngadagContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var res = await context.Ads.ToListAsync();
            foreach (var m in res)
            {
                m.Descriptions = m.Descriptions is not null ? Utils.Substring(m.Descriptions, 250) : "null";
            }
            return View(res);
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await context.Ads
                .Include(t => t.AdPhotos)
                .SingleAsync(s => s.Id == id);
            

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}