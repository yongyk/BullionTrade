using fyp.Data;
using Microsoft.AspNetCore.Mvc;
using fyp.Models;

namespace fyp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AppointmentSlotController : Controller
    {
        private readonly ApplicationDbContext _db;
        public AppointmentSlotController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<AppointmentSlot> appointmentSlotList = _db.AppointmentSlots.ToList();
            return View(appointmentSlotList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AppointmentSlot obj)
        {
            if (ModelState.IsValid)
            {
                _db.AppointmentSlots.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Slot created successfully";
                return RedirectToAction("Index", "AppointmentSlot");
            }
            return View();

        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            AppointmentSlot? slot = _db.AppointmentSlots.FirstOrDefault(c => c.Id == id);
            if (slot == null)
            {
                return NotFound();
            }
            return View(slot);
        }

        [HttpPost]
        public IActionResult Edit(AppointmentSlot obj)
        {
            if (ModelState.IsValid)
            {
                _db.AppointmentSlots.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "slot updated successfully";

                return RedirectToAction("Index", "AppointmentSlot");
            }
            return View();

        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            AppointmentSlot? slot = _db.AppointmentSlots.FirstOrDefault(c => c.Id == id);
            if (slot == null)
            {
                return NotFound();
            }
            return View(slot);
        }

        // get and post method must be same name if not same name need to explicitly tell asp.net core
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            AppointmentSlot? slot = _db.AppointmentSlots.Find(id);
            if (slot == null)
            {
                return NotFound();

            }

            _db.AppointmentSlots.Remove(slot);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";

            return RedirectToAction("Index", "AppointmentSlot");

        }
    }
}
