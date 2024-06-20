using Microsoft.AspNetCore.Mvc;
using ToDo.Data;

namespace ToDo.Web.Controllers
{
    public class PriorityController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PriorityController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult GetAll()
        {
            return Json(_context.Priorities.ToList());
        }
    }
}