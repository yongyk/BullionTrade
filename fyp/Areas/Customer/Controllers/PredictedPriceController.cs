using Microsoft.AspNetCore.Mvc;

namespace fyp.Areas.Customer.Controllers
{
    [Area("Customer")]

    public class PredictedPriceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
