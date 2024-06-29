
using fyp.Data;
using fyp.Models;
using fyp.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace fyp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =SD.Role_Admin)]
    public class ArticleController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ArticleController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Article> articleFromDb= _db.Articles.ToList(); 
            return View(articleFromDb);
        }
        public IActionResult Create()
        {
            Article model = new Article(); // Ensure it is initialized

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(Article obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    // string productPath= Path.Combine(wwwRootPath, fileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\article");
                    if (!string.IsNullOrEmpty(obj.ImageUrl))
                    {
                        var oldImagePath =
                           Path.Combine(wwwRootPath, obj.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    obj.ImageUrl = @"\images\article\" + fileName;
                    ;
                }
                //  obj.DateCreated = DateTime.Now.ToString("yyyy-MM-dd"); 
                _db.Articles.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Article created successfully";
                return RedirectToAction("Index", "Article");
            }
            return View();

        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Article? article = _db.Articles.FirstOrDefault(c => c.Id == id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }

        [HttpPost]
        public IActionResult Edit(Article obj)
        {
            if (ModelState.IsValid)
            {
                _db.Articles.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Article updated successfully";

                return RedirectToAction("Index", "Article");
            }
            return View();

        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Article? article = _db.Articles.FirstOrDefault(c => c.Id == id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }

        // get and post method must be same name if not same name need to explicitly tell asp.net core
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Article? obj = _db.Articles.Find(id);
            if (obj == null)
            {
                return NotFound();

            }
            
            var productToBeDeleted = _db.Articles.FirstOrDefault(u => u.Id == id);
            if (productToBeDeleted == null)
            {
                return NotFound();
            }
            
            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, productToBeDeleted.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _db.Articles.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Article deleted successfully";

            return RedirectToAction("Index", "Article");

        }
    }
}
