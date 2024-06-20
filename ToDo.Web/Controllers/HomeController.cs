using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToDo.Data;
using ToDo.Models;


namespace ToDo.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _context;



        public HomeController(ApplicationDbContext context)
        {
            _context = context;

        }

        //localhost/Home

        public IActionResult Index()
        {




            return View();
        }




    }
}
