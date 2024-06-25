using fyp.Data;
using fyp.Models;
using Microsoft.AspNetCore.Mvc;

namespace fyp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticleController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ArticleController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Article> articleFromDb= _db.Articles.ToList(); 
            return View(articleFromDb);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Article obj)
        {
            if (ModelState.IsValid)
            {
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

            _db.Articles.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Article deleted successfully";

            return RedirectToAction("Index", "Article");

        }
    }
}
