using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RRshop.Models;
using System.Security.Claims;

namespace RRshop.Controllers
{
    public class AccountController : Controller
    {
        private readonly rrshopContext _context;

        public AccountController(rrshopContext context)
        {
            _context = context;
        }

        // GET: AccountController
        [Authorize]
        public async Task<IActionResult> Index()
        {
            int _id = int.Parse(s: HttpContext.User.FindFirst("id").Value);
            User model = await _context.Users.FirstOrDefaultAsync(m => m.Id == _id);
            
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Register() => View();



        [HttpPost]
        public async Task<IActionResult> Register(User model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Ошибка регистрации ", e.Message);
                return View(model);
            }

        }


        public IActionResult Login(string returnUrl) => View();


        [HttpPost]
        public async Task<IActionResult> Login(User model)
        {
            if (!ModelState.IsValid) return View(model);
            User userDB;
            try
            {
                userDB = await _context.Users.FindAsync(model.Phone);

                if (model.Password == userDB.Password)
                {

                    var principal = GetClaimsPrincipalDefault(userDB);
                    await HttpContext.SignInAsync(principal);

                    return Redirect("/Account");
                }

                throw new Exception("Неверный пароль");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Ошибка при входе", e.Message);
                return View(model);
            }
        }


        private ClaimsPrincipal GetClaimsPrincipalDefault(User newUser)
        {
            var claims = new List<Claim>
            {
                new Claim("id", newUser.Id.ToString(), ClaimValueTypes.Integer),
                new Claim(ClaimTypes.MobilePhone, newUser.Phone),
                new Claim(ClaimTypes.Role, newUser.Role)
            };

            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimPrincipal = new ClaimsPrincipal(claimIdentity);

            return claimPrincipal;
        }

    }
}
