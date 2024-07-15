

using fyp.Data;
using fyp.Models;
using fyp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe.Checkout;
using System.Security.Claims;

namespace fyp.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _db;

        private readonly IEmailSender _emailSender; 
        [BindProperty]
        public ShoppingCartVM ShoppingCartVM { get; set; }      
        public ShoppingCartController(ApplicationDbContext db, IEmailSender emailSender)
        {
            _db = db;
            _emailSender = emailSender;
        }

		
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            ShoppingCartVM = new()
            {
                //newly added tolist()
                ShoppingCartList = _db.ShoppingCarts.Where(a=>a.ApplicationUserId == userId)
                .Include (p=>p.Product)
                .ToList(),

                OrderHeader=new ()
            };

            //newly added
            bool cartUpdated = false;


            foreach (var cart in ShoppingCartVM.ShoppingCartList)
            {
                //newly added
                var currentProduct = _db.Products.FirstOrDefault(p => p.Id == cart.ProductId);
                if (currentProduct == null || currentProduct.Quantity == 0)
                {
                    // Product no longer available, remove from cart
                    _db.ShoppingCarts.Remove(cart);
                    cartUpdated = true;
                    continue;
                }

                if (cart.Count > currentProduct.Quantity)
                {
                    // Adjust the cart quantity to match available stock
                    cart.Count = currentProduct.Quantity;
                    _db.ShoppingCarts.Update(cart);
                    cartUpdated = true;
                }//till here

                cart.Price = GetPrice(cart);
                ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }

            //newly added
            if (cartUpdated)
            {
                _db.SaveChanges();
                TempData["success"] = "Your cart has been updated due to changes in product availability.";
            }//till here

            return View(ShoppingCartVM);
           
        }
        

		
		private double GetPrice(ShoppingCart shoppingCart)
        {
            return shoppingCart.Product.Price;  
        }

        public IActionResult Plus (int cId)
        {
            var getCart = _db.ShoppingCarts.Include(c=>c.Product).FirstOrDefault(a => a.Id == cId);
            if (getCart != null)
            {
                if (getCart.Count < getCart.Product.Quantity)
                {
                    getCart.Count += 1;
                    _db.ShoppingCarts.Update(getCart);
                    _db.SaveChanges();
                }
                else
                {
                    TempData["success"] = "Maximum available quantity reached for this product.";

                }
            }
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
        public IActionResult Delete(int cId)
        {
            var getCart = _db.ShoppingCarts.FirstOrDefault(a => a.Id == cId);          
                // remove from cart
             _db.ShoppingCarts.Remove(getCart);            
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        
        public IActionResult Summary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            ShoppingCartVM = new()
            {
                ShoppingCartList = _db.ShoppingCarts.Where(a => a.ApplicationUserId == userId).Include(p => p.Product),
                OrderHeader = new()
            };
            ShoppingCartVM.OrderHeader.ApplicationUser = _db.ApplicationUsers.FirstOrDefault(u => u.Id == userId);

            ShoppingCartVM.OrderHeader.Name = ShoppingCartVM.OrderHeader.ApplicationUser.Name;
            ShoppingCartVM.OrderHeader.PhoneNumber = ShoppingCartVM.OrderHeader.ApplicationUser.PhoneNumber;
            ShoppingCartVM.OrderHeader.StreetAddress = ShoppingCartVM.OrderHeader.ApplicationUser.StreetAddress;
            ShoppingCartVM.OrderHeader.City = ShoppingCartVM.OrderHeader.ApplicationUser.City;
            ShoppingCartVM.OrderHeader.State = ShoppingCartVM.OrderHeader.ApplicationUser.State;
            ShoppingCartVM.OrderHeader.PostalCode = ShoppingCartVM.OrderHeader.ApplicationUser.PostalCode;



            foreach (var cart in ShoppingCartVM.ShoppingCartList)
            {
                cart.Price = GetPrice(cart);
                ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }
            return View(ShoppingCartVM);
        }
        
      
        
        [HttpPost]
        [ActionName("Summary")]
		public IActionResult SummaryPOST()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartVM.ShoppingCartList = _db.ShoppingCarts.Where(a => a.ApplicationUserId == userId).Include(p => p.Product).ToList();
            ShoppingCartVM.OrderHeader.ApplicationUserId = userId;

            ApplicationUser applicationUser = _db.ApplicationUsers.FirstOrDefault(u => u.Id == userId);

            //newly added gpt
			bool cartUpdated = false;


			foreach (var cart in ShoppingCartVM.ShoppingCartList)
			{
                //newly added gpt
				var currentProduct = _db.Products.FirstOrDefault(p => p.Id == cart.ProductId);
				if (currentProduct == null || currentProduct.Quantity == 0)
				{
					_db.ShoppingCarts.Remove(cart);
					cartUpdated = true;
					continue;
				}//till here
                //newly added gpt
				if (cart.Count > currentProduct.Quantity)
				{
					cart.Count = currentProduct.Quantity;
					_db.ShoppingCarts.Update(cart);
					cartUpdated = true;
				}//till here


				cart.Price = GetPrice(cart);
				ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
			}
            //newly added gpt
			if (cartUpdated)
			{
				_db.SaveChanges();
				TempData["success"] = "Your cart has been updated due to changes in product availability.";
				return RedirectToAction("Summary"); // Redirect to re-render the updated cart
			}//till here

			//normal user
			ShoppingCartVM.OrderHeader.PaymentStatus = "Pending";
			ShoppingCartVM.OrderHeader.OrderStatus = "Pending";

            _db.OrderHeaders.Add(ShoppingCartVM.OrderHeader);
            _db.SaveChanges();

            foreach(var cart in ShoppingCartVM.ShoppingCartList)
            {
                OrderDetail orderDetail = new()
                {
                    ProductId = cart.ProductId,
                    OrderHeaderId = ShoppingCartVM.OrderHeader.Id,
                    Price = cart.Price,
                    Count = cart.Count
                };
                _db.OrderDetails.Add(orderDetail);
                _db.SaveChanges();
            }
            //normal user
            //stripe logic

            var domains = "https://localhost:7293/";
			var options = new SessionCreateOptions
			{
				SuccessUrl = domains+ $"customer/ShoppingCart/OrderConfirmation?id={ShoppingCartVM.OrderHeader.Id}",
                CancelUrl = domains+ $"customer/ShoppingCart/index",
				LineItems = new List<SessionLineItemOptions>(),
				Mode = "payment",
			};

            foreach(var item in ShoppingCartVM.ShoppingCartList)
            {
                var sessionLineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Price * 100), //$20.50 =>2050
                        Currency = "myr",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Product.Name
                        }
                    },
                    Quantity = item.Count
                };
                options.LineItems.Add(sessionLineItem);
            }
			var service = new SessionService();
			Session session=service.Create(options);
			var orderHeaderToUpdate =  _db.OrderHeaders.Find(ShoppingCartVM.OrderHeader.Id);
			if (orderHeaderToUpdate != null)
			{
				orderHeaderToUpdate.SessionId = session.Id;
				orderHeaderToUpdate.PaymentIntentId = session.PaymentIntentId;
                //add date here

				_db.OrderHeaders.Update(orderHeaderToUpdate);
			     _db.SaveChanges();
                Response.Headers.Add("Location", session.Url);
                return new StatusCodeResult(303);
			}
			//_db.OrderHeaders.Update(ShoppingCartVM.OrderHeader.Id,session.Id)
            
			return RedirectToAction("OrderConfirmation", new  { id=ShoppingCartVM.OrderHeader.Id});
		}
            
        public IActionResult OrderConfirmation(int id)
        {
            OrderHeader? orderHeader = _db.OrderHeaders.Include(a => a.ApplicationUser).FirstOrDefault(u => u.Id == id);
            var service = new SessionService();
            Session session = service.Get(orderHeader.SessionId);
			var getOrderHeader = _db.OrderHeaders.FirstOrDefault(u => u.Id == id);

			if (session.PaymentStatus.ToLower() == "paid")
            {
               //var getOrderHeader= _db.OrderHeaders.FirstOrDefault(u => u.Id == id);
                if(getOrderHeader != null) 
                { 
                    getOrderHeader.OrderStatus= session.Status;
                  //  if(!string.IsNullOrEmpty(session.PaymentStatus)) {
                        getOrderHeader.PaymentStatus = session.PaymentStatus;
                  //  }
                }
                _db.OrderHeaders.Update(getOrderHeader);

                //newly added
                var orderDetails = _db.OrderDetails.Where(od => od.OrderHeaderId == id).ToList();
                foreach (var detail in orderDetails)
                {
                    var product = _db.Products.FirstOrDefault(p => p.Id == detail.ProductId);
                    if (product != null)
                    {
                        product.Quantity -= detail.Count;
                        _db.Products.Update(product);
                    }
                }
                _db.SaveChanges();

            }
            // if(!)
            //getOrderHeader.SessionId = session.ClientReferenceId;
            getOrderHeader.PaymentIntentId= session.PaymentIntentId;
            getOrderHeader.PaymentDate = DateTime.Now;
            _db.OrderHeaders.Update(getOrderHeader);
            _db.SaveChanges();


            _emailSender.SendEmailAsync(
                orderHeader.ApplicationUser.Email, "New Order - BullionTrade",
                $"<p> New order is created - {orderHeader.Id}</p>"
                );

            List<ShoppingCart>shoppingCarts= _db.ShoppingCarts.Where(u=>u.ApplicationUserId == orderHeader.ApplicationUserId).ToList();
            _db.ShoppingCarts.RemoveRange(shoppingCarts);
            _db.SaveChanges();  
            return View(id);  
        }
        
    }
        
}
