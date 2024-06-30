using fyp.Services;
using Microsoft.AspNetCore.Mvc;

namespace fyp.Areas.Customer.Controllers
{
    [Area("Customer")]

    public class GoldPriceController : Controller
    {
        private readonly GoldPriceService _goldPriceService;

        public GoldPriceController(GoldPriceService goldPriceService)
        {
            _goldPriceService = goldPriceService;
        }

        public async Task<IActionResult> Index()
        {
            var pricePoints = await _goldPriceService.GetGoldPriceIntradayAsync();
            decimal latestPrice = pricePoints.OrderByDescending(p => p.Key).FirstOrDefault().Value;
            return View(latestPrice); // Pass only the latest price to the view
        }
      
    }
}
