using fyp.Data;
using fyp.Models;
using fyp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;


namespace fyp.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Product()
        { 
            IEnumerable<Product> prod = _db.Products.Include(p => p.Category).ToList();  
            return View(prod);  
        }

        public IActionResult Details(int id)
        {
            //maybe this causes weird err
            ShoppingCart cart = new()
            {
                Product = _db.Products.Include(p => p.Category).FirstOrDefault(u => u.Id == id),
                Count = 1,
                ProductId = id
            };
            //Product? prod = _db.Products.Include(p => p.Category).FirstOrDefault(u=>u.Id==id);

            return View(cart);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId= claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            //Product? prod = _db.Products.Include(p => p.Category).FirstOrDefault(u=>u.Id==id);
            shoppingCart.ApplicationUserId = userId;
            //new things added 
            shoppingCart.Id = 0;

            ShoppingCart cartFromDb = _db.ShoppingCarts.FirstOrDefault(u => u.ApplicationUserId == userId && u.ProductId ==shoppingCart.ProductId);

            if(cartFromDb != null)
            {
                //shopping cart exists
                //update the shopping cart counts
                cartFromDb.Count += shoppingCart.Count;
                _db.ShoppingCarts.Update(cartFromDb);
            }
            else
            {
                //add cart record
                _db.ShoppingCarts.Add(shoppingCart);

            }
            TempData["success"] = "cart updated successfully";

           // _db.ShoppingCarts.Add(shoppingCart);
            _db.SaveChanges();
          
            return RedirectToAction("Product");
        }
     
        [HttpGet]
        public IActionResult Selling()
        {
           
            SellingVM sellingVM = new()
            {
                AppointmentList = _db.AppointmentSlots.Select(s => new SelectListItem
                {
                    Text = $"{s.Date} {s.Time}",
                    Value = s.Id.ToString()

                }),
                MetalPurity = new List<SelectListItem>
                 {
                  new SelectListItem { Text = "999", Value = "999" },
                  new SelectListItem { Text = "916", Value = "916" }
                  },
            Selling = new Selling()
            };
            return View(sellingVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Selling(SellingVM sellingVM)
        {
           
            if (ModelState.IsValid)
            {
               
                _db.Sellings.Add(sellingVM.Selling);
                _db.SaveChanges();
                TempData["success"] = "New appointment created successfully.";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                sellingVM.AppointmentList = _db.AppointmentSlots.Select(s => new SelectListItem
                {
                    Text = $"{s.Date} {s.Time}",
                    Value = s.Id.ToString()
                });
                sellingVM.MetalPurity = new List<SelectListItem>
                  {
                    new SelectListItem { Text = "999", Value = "999" },
                     new SelectListItem { Text = "916", Value = "916" }
                     };

                return View(sellingVM);
            }
          
        }
       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
