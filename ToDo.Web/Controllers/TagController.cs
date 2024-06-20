using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDo.Data;
using ToDo.Models;
using ToDo.Repository.Shared.Abstract;

namespace ToDo.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TagController : Controller
    {
        private readonly IRepository<Tag> _tagRepository;

        public TagController(IRepository<Tag> tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult GetAllTags()
        {
            return Json(new { data = _tagRepository.GetAll() });
        }

        [HttpPost]
        public IActionResult DeleteTag(int id)
        {
            _tagRepository.DeleteById(id);
            
            return Ok("İşlem Başarılı");
        }

        [HttpPost]
        public IActionResult Add(Tag tag)
        {
          
            return Ok(_tagRepository.Add(tag));

        }

        [HttpPost]
        public IActionResult Update(Tag tag)
        {
            _tagRepository.Update(tag);
            return Ok(tag);

        }
    }
}
