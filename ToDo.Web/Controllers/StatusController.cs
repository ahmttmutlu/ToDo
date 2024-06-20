using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.Data;
using ToDo.Models;
using ToDo.Repository.Shared.Abstract;

namespace ToDo.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StatusController : Controller
    {
       private readonly IRepository<Status> _statusRepository;

        public StatusController(IRepository<Status> statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult Index2()
        //{
        //    return View(_context.Statuses.ToList());
        //}
        [AllowAnonymous]
        public IActionResult GetAllStatus()
        {
           
            return Json(new { data = _statusRepository.GetAll() });
        }

        [HttpPost]
        public IActionResult DeleteStatus(int id)
        {
            _statusRepository.DeleteById(id);
            return Ok("İşlem Başarılı");
        }


        //public IActionResult DeleteStatus2(int id)
        //{
        //    var item = _context.Statuses.Find(id);
        //    _context.Statuses.Remove(item);
        //    _context.SaveChanges();
        //    return RedirectToAction("Index2");
        //}

        //public IActionResult Add()
        //{
        //    return View();
        //}

        [HttpPost]
        public IActionResult Add(Status status)
        {
            
            return Ok(_statusRepository.Add(status));
        }

        //[HttpPost]
        //public IActionResult Add2(Status status)
        //{
        //    _context.Statuses.Add(status);
        //    _context.SaveChanges();
        //    return RedirectToAction("Index2");
        //}

        [HttpPost]
        public IActionResult Update(Status status)
        {
            _statusRepository.Update(status);
            return Ok(status);

        }



        //public IActionResult Update2(int id)
        //{
        //    return View(_context.Statuses.Find(id));
        //}
        //[HttpPost]
        //public IActionResult Update2(Status status)
        //{

        //    if (status != null && ModelState.IsValid)
        //    {
        //        _context.Statuses.Update(status);
        //        _context.SaveChanges();
        //    }
        //    return RedirectToAction("Index2");

        //}
    }
}

