using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDo.Data;
using ToDo.Models;

namespace ToDo.Web.Controllers
{

    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "User")]
        public IActionResult Profile()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));





            return View(_context.Users.Find(userId));
        }

        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(AppUser user)
        {
            if (user != null)
            {
                AppUser appUser = _context.Users.FirstOrDefault(u => u.Name == user.Name && u.Password == user.Password);
                if (appUser != null)
                {
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()));
                    claims.Add(new Claim(ClaimTypes.Name, appUser.Name));
                    claims.Add(new Claim(ClaimTypes.Role, appUser.IsAdmin ? "Admin" : "User"));

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties { IsPersistent = true });


                    if (appUser.IsAdmin)
                        return RedirectToAction("Index", "Home");
                    else
                        return RedirectToAction("Index", "Todo");

                }


            }

            return RedirectToAction("Login");

        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult GetAll()
        {
            // var todos = _context.Todos.ToList();


            //var result = _context.Users.Select(user => new
            //{
            //    user.Id,
            //    user.Name,
            //    user.Password,
            //    TodoCount=todos.Where(t=>t.AppUserId==user.Id).ToList().Count(),
            //}) ;

            var result = _context.Users.Select(user => new
            {
                user.Id,
                user.Name,
                user.Password,
                user.IsAdmin,
                user.Photo,
                TodoCount = user.Todos.Count
            });

            return Json(new { data = result });


        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Add(AppUser user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok(user);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Delete(AppUser user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
            return Ok(user.Id);

        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Update(AppUser user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
            return Ok(user);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(AppUser user)
        {
            if (user.Id == int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)))
            {
                var foto = HttpContext.Request.Form.Files[0];
                FileInfo fotofile = new FileInfo(foto.FileName);
                string dosyaYolu = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "UserPhotos");

                if (!Directory.Exists(dosyaYolu))
                    Directory.CreateDirectory(dosyaYolu);

                string dosyaAdi = user.Id + "-" + user.Name + "-" + Guid.NewGuid().ToString().Substring(0, 8) + fotofile.Extension;
                using (var stream = new FileStream(Path.Combine(dosyaYolu, dosyaAdi), FileMode.Create))
                {
                    await foto.CopyToAsync(stream);
                }

                user.Photo = dosyaAdi;


                _context.Users.Update(user);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }





        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult GetById(int id)
        {
            return Json(_context.Users.Where(u => u.Id == id).Select(u => new
            {
                u.Id,
                u.Name,
                u.Password,
                u.IsAdmin,
                TodoCount = u.Todos.Count

            }).First());
        }

    }
}
