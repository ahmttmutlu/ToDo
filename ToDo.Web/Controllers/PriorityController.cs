using Microsoft.AspNetCore.Mvc;
using ToDo.Data;
using ToDo.Models;
using ToDo.Repository.Shared.Abstract;

namespace ToDo.Web.Controllers
{
    public class PriorityController : Controller
    {
        private readonly IRepository<Priority> _repo;

        public PriorityController(IRepository<Priority> repo)
        {
            _repo = repo;
        }

        public IActionResult GetAll()
        {
            return Json(_repo.GetAll());
        }
    }
}