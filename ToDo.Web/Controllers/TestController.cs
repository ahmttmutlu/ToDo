//using Microsoft.AspNetCore.Mvc;
//using ToDo.Utility;

//namespace ToDo.Web.Controllers
//{
//    public class TestController : Controller
//    {
//        public IActionResult Index()
//        {

//            return View();
//        }

//        [HttpPost]
//        public async Task<IActionResult> Deneme()
//        {

//            string id = HttpContext.Request.Form["ID"];
//            string name = HttpContext.Request.Form["Name"];

//            var foto = HttpContext.Request.Form.Files[0];

//            string mimeType = Helper.GetMimeType(foto);

//            FileInfo fotofile = new FileInfo(foto.FileName);
            
//            //mime type
            

//            string dosyaYolu = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "UserPhotos"); //nereye kaydetceksek orayı belirliyor


//            if (!Directory.Exists(dosyaYolu))
//            {
//                Directory.CreateDirectory(dosyaYolu);
//            }

//            string dosyaAdi = id + "-" + name + "-" + Guid.NewGuid().ToString().Substring(0, 8) + fotofile.Extension;

//            using (var stream = new FileStream(Path.Combine(dosyaAdi,dosyaYolu), FileMode.Create))
//            {
//                await foto.CopyToAsync(stream);
//            }

//            return Ok();
//        }
//    }
//}
