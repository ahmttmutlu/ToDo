using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ToDo.Data;
using ToDo.Models;


namespace ToDo.Web.Controllers
{
    [Authorize(Roles = "User")]
    public class TodoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TodoController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Index2()
        {
            return View();
        }

        public IActionResult GetAll()
        {

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return Json(new
            {
                data = _context.Todos.Where(t => t.AppUserId == userId && t.Status.Name != "Tamamlandı").Select(t => new
                {
                    t.Id,
                    t.Name,
                    t.Description,
                    PriorityColor = t.Priority.Color,
                    StatusName = t.Status.Name,
                    Tags = t.Tags.Select(tag => new { tag.Name, tag.Id })
                    




                })
            });
        }

        public IActionResult RemoveTag(int todoId, int tagId)
        {
            Todo todo = _context.Todos.Include(t => t.Tags).FirstOrDefault(t => t.Id == todoId);
            Tag tag = _context.Tags.Find(tagId);

            todo.Tags.Remove(tag);
            _context.Todos.Update(todo);
            _context.SaveChanges();

            return Ok("işlem başarılı");

        }

        public IActionResult Add(Todo todo, int[] tags)
        {
            todo.Tags = _context.Tags.Where(t => tags.Contains(t.Id)).ToList();
            todo.AppUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            _context.Todos.Add(todo);
            _context.SaveChanges();

            return Ok();
        }

        //bu metodu http post ile de yapılabilir. Ancak çeşit olması açısından bunu get ile çalıştırıyoruz bu seferlik.
        public IActionResult GetById(int id)
        {

            return Json(_context.Todos.Where(t => t.Id == id).Select(t => new Todo
            {
                Name = t.Name,
                Id = t.Id,
                Description = t.Description,
                Status = t.Status,
                Tags = t.Tags
            }).First());

        }
        public IActionResult Update(Todo todo, int[] tags)
        {
            Todo asil = _context.Todos.Include(t => t.Tags).FirstOrDefault(t => t.Id == todo.Id);
            asil.Tags = _context.Tags.Where(t => tags.Contains(t.Id)).ToList();
            asil.Name = todo.Name;
            asil.Description = todo.Description;
            asil.StatusId = todo.StatusId;


            _context.Todos.Update(asil);
            _context.SaveChanges();
            return Ok();

            //json base65string
            //javascript tarafından arka yuze FormData
        }

        [HttpPost]
        public IActionResult Complete(int todoId)
        {
            Todo todo = _context.Todos.Find(todoId);

            todo.Status = _context.Statuses.FirstOrDefault(s => s.Name == "Tamamlandı");

            _context.Todos.Update(todo);
            _context.SaveChanges();
            return Ok();


        }
    }
}
