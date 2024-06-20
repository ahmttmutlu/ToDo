using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ToDo.Data;
using ToDo.Models;
using ToDo.Repository.Abstract;

namespace ToDo.Web.Controllers
{

    public class UserController : Controller
    {
        private readonly IUserRepository _repo;

        public UserController(IUserRepository repo)
        {
            _repo = repo;
        }

        [Authorize(Roles = "User")]
        public IActionResult Profile()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));





            return View(_repo.GetById(userId));
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
                AppUser appUser = _repo.GetFirstOrDefault(u => u.Name == user.Name && u.Password == user.Password);
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

            return Json(new { data = _repo.GetAllWithTodoCount() });


        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Add(AppUser user)
        {

            return Ok(_repo.Add(user));
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Delete(AppUser user)
        {

            return Ok(_repo.Delete(user.Id) is object);

        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Update(AppUser user)
        {

            return Ok(_repo.Update(user));
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


                _repo.Update(user);
            }
            return RedirectToAction("Index", "Home");
        }





        //[Authorize(Roles = "Admin")]
        //[HttpPost]
        //public IActionResult GetById(int id)
        //{
        //    return Json(_context.Users.Where(u => u.Id == id).Select(u => new
        //    {
        //        u.Id,
        //        u.Name,
        //        u.Password,
        //        u.IsAdmin,
        //        TodoCount = u.Todos.Count

        //    }).First());
        //}

    }
}
