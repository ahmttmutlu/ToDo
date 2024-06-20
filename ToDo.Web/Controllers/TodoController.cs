using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ToDo.Data;
using ToDo.Models;
using ToDo.Repository.Abstract;


namespace ToDo.Web.Controllers
{
    [Authorize(Roles = "User")]
    public class TodoController : Controller
    {
       private readonly ITodoRepository _todoRepository;

        public TodoController(ITodoRepository repository)
        {
            _todoRepository = repository;
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

            
            return Json(new
            {
                data = _todoRepository.GetAll(int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)))
            });
        }

        public IActionResult RemoveTag(int todoId, int tagId)
        {
            _todoRepository.RemoveTag(todoId, tagId);
            return Ok("işlem başarılı");

        }

        public IActionResult Add(Todo todo, int[] tags)
        {
            _todoRepository.Add(todo, int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)), tags);
            return Ok();
        }

        //bu metodu http post ile de yapılabilir. Ancak çeşit olması açısından bunu get ile çalıştırıyoruz bu seferlik.
        public IActionResult GetById(int id)
        {

            return Json(_todoRepository.GetById(id));

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
