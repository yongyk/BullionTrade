using fyp.Models;
using fyp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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

        public async Task<IActionResult> Index(string date = null)
        {
            try
            {
                var price = await _goldPriceService.GetGoldPriceAsync("XAU", "USD", date);
                return View(price);
            }
            catch (Exception ex)
            {
                // Handle error (log and show error message)
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

    }
}
/*
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
        */
