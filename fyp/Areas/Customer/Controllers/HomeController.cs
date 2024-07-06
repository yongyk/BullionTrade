using fyp.Data;
using fyp.Models;
using fyp.Models.ViewModels;
using fyp.Services;
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
        private readonly EmailSender _emailSender;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db, EmailSender emailSender)
        {
            _logger = logger;
            _db = db;
            _emailSender = emailSender;
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
        [Authorize]

        [ValidateAntiForgeryToken]
        public IActionResult Selling(SellingVM sellingVM)
        {
           
            if (ModelState.IsValid)
            {
               
                _db.Sellings.Add(sellingVM.Selling);
                _db.SaveChanges();
                //new added
                var appointmentSlot =_db.AppointmentSlots.FirstOrDefault(a => a.Id == sellingVM.Selling.AppointmentSlotId);

                // Send confirmation email
                /*
                string subject = "Appointment Confirmation";
                string message = "Your appointment for selling gold has been confirmed ";
                var task = _emailSender.SendEmailAsync(sellingVM.Selling.Email, subject, message);
                task.Wait();
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
                */
                // Send confirmation email
               
                    string subject = "Appointment Confirmation";
                    string message = $@"
                <p>Dear Customer,</p>
                <p>Your appointment for selling gold has been confirmed.</p>
                <p><strong>Appointment Details:</strong></p>
                <ul>
                    <li>Date: {appointmentSlot.Date}</li>
                    <li>Time: {appointmentSlot.Time}</li>
                    <li>Product Purity: {sellingVM.Selling.ProductPurity}</li>
                    <li>Weight: {sellingVM.Selling.Weight}</li>
                </ul>
                <p>Please make sure to bring a valid ID and your gold items to the appointment.</p>
                <p>If you have any questions or need to reschedule, please contact us at <a href='mailto:support@example.com'>support@example.com</a>.</p>
                <p>Thank you for choosing our service.</p>
                <p>Sincerely,<br>BullionTrade</p>";


                //$"Your appointment for selling gold has been confirmed  for {appointmentSlot.Date} at {appointmentSlot.Time}.";

                try
                    {
                        Task.Run(() => _emailSender.SendEmailAsync(sellingVM.Selling.Email, subject, message)).Wait();
                        TempData["success"] = "New appointment created successfully.";
                    }
                    catch (Exception)
                    {
                        TempData["error"] = "Appointment created, but failed to send confirmation email.";
                    }

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
       public IActionResult IndexArticle()
        {
            List<Article> getArticle= _db.Articles.ToList();
            return View(getArticle);
        }
        
        public IActionResult ArticleDetails(int? id)
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
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
