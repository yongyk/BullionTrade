using fyp.Data;
using Microsoft.AspNetCore.Mvc;
using fyp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using fyp.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
namespace fyp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
            _db.Products.Include(u => u.Category);
        }
        public IActionResult Index()
        {
            // List<Product> prod= _db.Products.ToList();
            List<Product> prod = _db.Products.Include(p => p.Category).ToList();


            return View(prod);

        }
        //update &insert
        public IActionResult Upsert(int? id)
        {

            //IEnumerable<SelectListItem> CategoryList =
            //ViewBag.CategoryList = CategoryList;
            ProductVM productVM = new()
            {
                CategoryList = _db.Categories.Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                BrandList = new List<SelectListItem>  // Example hardcoded list
        {
            new SelectListItem{ Text="Pamp", Value="Pamp"},
            new SelectListItem{ Text="Valcambi", Value="Valcambi"},
            new SelectListItem{ Text="RCM", Value="RCM"},
             new SelectListItem{ Text="PerthMint", Value="PerthMint"},



        },
                MetalList = new List<SelectListItem>
        {
            new SelectListItem{ Text="Gold", Value="Gold"},
            new SelectListItem{ Text="Silver", Value="Silver"},
            new SelectListItem{ Text="Bronze", Value="Bronze"}
        },
                PurityList = new List<SelectListItem>
        {
            new SelectListItem{ Text="999", Value="999"},
            new SelectListItem{ Text="916", Value="916"}
        },

                Product = new Product()
            };
            if (id == null || id == 0)
            {
                //create
                return View(productVM);
            }
            else
            {
                //update
                productVM.Product = _db.Products.FirstOrDefault(u => u.Id == id);
                // productVM.Product = _db.Products.Include(p => p.Category).FirstOrDefault(u => u.Id == id);
                if (productVM.Product == null)
                {
                    return NotFound();
                }
                return View(productVM);
            }
        }
        [HttpPost]
        public IActionResult Upsert(ProductVM prod, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                //webrootpath is www.root folder
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    // string productPath= Path.Combine(wwwRootPath, fileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");
                    if (!string.IsNullOrEmpty(prod.Product.ImageUrl))
                    {
                        var oldImagePath =
                           Path.Combine(wwwRootPath, prod.Product.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    prod.Product.ImageUrl = @"\images\product\" + fileName;
                    ;
                }


                if (prod.Product.Id == 0)
                {
                    _db.Products.Add(prod.Product);
                }
                else
                {
                    _db.Products.Update(prod.Product);
                }
                _db.SaveChanges();
                TempData["success"] = "new product created successfully.";
                return RedirectToAction("Index", "Product");
            }
            else
            {

                prod.CategoryList = _db.Categories.Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });


                return View(prod);
            }

        }
        /*
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
        */
        /*
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? prod = _db.Products.FirstOrDefault(c => c.Id == id);
            //Product? prod = _db.Products.Include(p => p.Category).FirstOrDefault(c => c.Id == id);

            if (prod == null)
            {
                return NotFound();
            }
            return View(prod);
        }
        */
        // get and post method must be same name if not same name need to explicitly tell asp.net core
        /*
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

        */

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> prod = _db.Products.Include(p => p.Category).ToList();
            return Json(new {data = prod});
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return Json(new { success = false, message = "No ID provided" });
            }
          //  try
           // {
                var productToBeDeleted = _db.Products.FirstOrDefault(u => u.Id == id);
                if (productToBeDeleted == null)
                {
                    return Json(new { success = false, message = "Error while deleting" });
                }
                var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, productToBeDeleted.ImageUrl.TrimStart('\\'));

                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }

                _db.Products.Remove(productToBeDeleted);
                _db.SaveChanges();
                return Json(new { success = true, message = "Delete successful" });
            }
            /*
            catch (Exception ex)
            {
                _logger.LogError("Error deleting product with ID {ProductId}: {Error}", id, ex.Message);
                return Json(new { success = false, message = $"Error while deleting: {ex.Message}" });
            }
            */
        }
        #endregion

    }
//}