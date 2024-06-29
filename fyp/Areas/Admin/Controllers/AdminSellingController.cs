using fyp.Data;
using fyp.Models;
using fyp.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fyp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles=SD.Role_Admin)]
    public class AdminSellingController : Controller
    {
        private readonly ApplicationDbContext _db;
        public AdminSellingController(ApplicationDbContext db)
        {
            _db=db;
            _db.Sellings.Include(u => u.ApplicationSlot);
           
        }
        public IActionResult Index()
        {
            List<Selling> sell = _db.Sellings.Include(u => u.ApplicationSlot).ToList();
            return View(sell);
        }
    }
}
