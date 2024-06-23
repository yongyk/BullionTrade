using fyp.Data;
using fyp.Models;
using fyp.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fyp.Areas.Admin.Controllers
{
	[Area("Admin")]

	public class OrderController : Controller
	{
		private readonly ApplicationDbContext _db;
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
			OrderVM orderVM= new()
			{
				OrderHeader = _db.OrderHeaders.Include("ApplicationUser").FirstOrDefault(u => u.Id == orderId),
				OrderDetail = _db.OrderDetails.Where(u => u.OrderHeaderId == orderId).Include("Product").ToList()
			};
			return View(orderVM);
		}

		#region API CALLS
		[HttpGet]
		public IActionResult GetAll()
		{
			List<OrderHeader> orderHeaderList = _db.OrderHeaders.Include(p => p.ApplicationUser).ToList();
			return Json(new { data = orderHeaderList });
		}

        #endregion

    }
}
