using fyp.Data;
using Microsoft.AspNetCore.Mvc;
using fyp.Models;
namespace fyp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
           List<Product> prod= _db.Products.ToList();   
            return View(prod);  

        }
        public IActionResult Create()
        {
            return View();  
        }
        [HttpPost]
        public IActionResult Create(Product prod)
        {
            if (ModelState.IsValid)
            {
                _db.Products.Add(prod);
                _db.SaveChanges();
                TempData["success"] = "new product created successfully.";
               return RedirectToAction("Index", "Product");
            }
            return View();
           
        }
        public IActionResult Edit(int? id)
        {
            if(id == null || id ==0 )
            {
                return NotFound();

            }
            Product? prod = _db.Products.FirstOrDefault(x => x.Id == id);  
            if(prod == null) 
            {
                return NotFound();

            }
            return View(prod);
        }
        [HttpPost]
        public IActionResult Edit(Product prod) 
        { 
            if (ModelState.IsValid)
            {
                _db.Products.Update(prod);
                _db.SaveChanges();
                TempData["success"] = "Product updated successfully";
                return RedirectToAction("Index");
            }
            return View();  
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product?prod = _db.Products.FirstOrDefault(c => c.Id == id);
            if (prod == null)
            {
                return NotFound();
            }
            return View(prod);
        }

        // get and post method must be same name if not same name need to explicitly tell asp.net core
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Product? obj = _db.Products.Find(id);
            if (obj == null)
            {
                return NotFound();

            }

            _db.Products.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Product deleted successfully";

            return RedirectToAction("Index");

        }
    }
}
