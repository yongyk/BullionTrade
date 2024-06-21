/*

using fyp.Data;
using fyp.Models;
using Microsoft.AspNetCore.Mvc;

namespace fyp.Areas.Customer.Controllers
{
    public class ApplicationUserController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ApplicationUserController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<ApplicationUser> objApplicationUserList = _db.ApplicationUsers.ToList();
            return View(objApplicationUserList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ApplicationUser obj)
        {
            if (ModelState.IsValid)
            {
                _db.ApplicationUsers.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "ApplicationUser created successfully";
                return RedirectToAction("Index");
            }
            return View();

        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            ApplicationUser? applicationUser = _db.ApplicationUsers.FirstOrDefault(c => c.Id == id.ToString());
            if (applicationUser == null)
            {
                return NotFound();
            }
            return View(applicationUser);
        }

        [HttpPost]
        public IActionResult Edit(ApplicationUser obj)
        {
            if (ModelState.IsValid)
            {
                _db.ApplicationUsers.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "ApplicationUser updated successfully";

                return RedirectToAction("Index", "ApplicationUser");
            }
            return View();

        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            ApplicationUser? applicationUser = _db.ApplicationUsers.FirstOrDefault(c => c.Id == id.ToString());
            if (applicationUser == null)
            {
                return NotFound();
            }
            return View(applicationUser);
        }

        // get and post method must be same name if not same name need to explicitly tell asp.net core
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            ApplicationUser? obj = _db.ApplicationUsers.Find(id);
            if (obj == null)
            {
                return NotFound();

            }

            _db.ApplicationUsers.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "ApplicationUser deleted successfully";

            return RedirectToAction("Index", "ApplicationUser");

        }
    }
}

*/