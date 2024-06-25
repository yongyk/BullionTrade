using fyp.Data;
using fyp.Models;
using fyp.Models.ViewModels;
using fyp.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace fyp.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize]
	public class OrderController : Controller
	{
		private readonly ApplicationDbContext _db;
		[BindProperty]
		public OrderVM OrderVM { get; set; }	
        public OrderController(ApplicationDbContext db)
        {
			_db = db;
        }
        public IActionResult Index()
		{
            List<OrderHeader> orderHeaderList = _db.OrderHeaders.Include(p => p.ApplicationUser).ToList();

            return View(orderHeaderList);
		}
		public IActionResult Details(int orderId)
		{
			OrderVM = new()
			{
				OrderHeader = _db.OrderHeaders.Include("ApplicationUser").FirstOrDefault(u => u.Id == orderId),
				OrderDetail = _db.OrderDetails.Where(u => u.OrderHeaderId == orderId).Include("Product").ToList()
			};
			return View(OrderVM);
		}


		[HttpPost]
		[Authorize(Roles =SD.Role_Admin)]

        public IActionResult UpdateOrderDetails(int orderId)
        {
			var getOrderHeader = _db.OrderHeaders.FirstOrDefault(u => u.Id == OrderVM.OrderHeader.Id);

            getOrderHeader.OrderDate = DateTime.Now;
            getOrderHeader.ShippingDate = DateTime.Now;

            getOrderHeader.Carrier = OrderVM.OrderHeader.Carrier;
			
			   getOrderHeader.TrackingNumber = OrderVM.OrderHeader.TrackingNumber;
            
			_db.OrderHeaders.Update(getOrderHeader);
			_db.SaveChanges();
			TempData["Success"] = "Order details Updated successfully";
			return RedirectToAction(nameof(Details), new {orderId= getOrderHeader});
        }

        public IActionResult OrderReport()
		{
            List<OrderHeader> orderHeaderList = _db.OrderHeaders.Include(p => p.ApplicationUser).ToList();

            return View(orderHeaderList);
        }

        #region API CALLS
        [HttpGet]
		public IActionResult GetAll()
		{
			IEnumerable<OrderHeader> objOrderHeaders;
			if (User.IsInRole(SD.Role_Admin))
			{
				objOrderHeaders = _db.OrderHeaders.Include(u => u.ApplicationUser).ToList();
			}
			else
			{
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

				objOrderHeaders = _db.OrderHeaders.Where(u => u.ApplicationUserId == userId).Include("ApplicationUser");
            }
			//List<OrderHeader> orderHeaderList = _db.OrderHeaders.Include(p => p.ApplicationUser).ToList();
			return Json(new { data = objOrderHeaders });
		}

        #endregion

    }
}
