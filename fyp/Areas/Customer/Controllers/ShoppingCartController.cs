

using fyp.Data;
using fyp.Models;
using fyp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace fyp.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ShoppingCartVM ShoppingCartVM { get; set; }      
        public ShoppingCartController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            ShoppingCartVM = new()
            {
                ShoppingCartList = _db.ShoppingCarts.Where(a=>a.ApplicationUserId == userId).Include (p=>p.Product)
            };

            
            foreach(var cart in ShoppingCartVM.ShoppingCartList)
            {
                cart.Price = GetPrice(cart);
                ShoppingCartVM.OrderTotal += (cart.Price * cart.Count);
            }
            return View(ShoppingCartVM);
           
        }
        
        private double GetPrice(ShoppingCart shoppingCart)
        {
            return shoppingCart.Product.Price;  
        }

        public IActionResult Plus (int cId)
        {
            var getCart = _db.ShoppingCarts.FirstOrDefault(a => a.Id == cId);
            getCart.Count += 1;
            _db.ShoppingCarts.Update(getCart);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Minus(int cId)
        {
            var getCart = _db.ShoppingCarts.FirstOrDefault(a => a.Id == cId);
            if (getCart.Count <= 1)
            {
                // remove from cart
                _db.ShoppingCarts.Remove(getCart);  
            }
            else
            {
                getCart.Count -= 1;
                _db.ShoppingCarts.Update(getCart);
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


        /*
        public IActionResult Create()
        {
            return View(new ShoppingCart());
        }
        public IActionResult Create(ShoppingCart obj)
        {
            if (ModelState.IsValid)
            {
                _db.ShoppingCarts.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "cart item added successfully";
                return RedirectToAction("Index", "ShoppingCart");
            }
            return View();

        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? category = _db.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Cart updated successfully";

                return RedirectToAction("Index", "Category");
            }
            return View();

        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            ShoppingCart? shoppingCart = _db.ShoppingCarts.FirstOrDefault(c => c.Id == id);
            if (shoppingCart == null)
            {
                return NotFound();
            }
            return View(shoppingCart);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            ShoppingCart? obj = _db.ShoppingCarts.Find(id);
            if (obj == null)
            {
                return NotFound();

            }

            _db.ShoppingCarts.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "cart item deleted successfully";

            return RedirectToAction("Index");

        }
        */
    }
}
