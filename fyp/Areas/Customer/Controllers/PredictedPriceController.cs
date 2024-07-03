using Microsoft.AspNetCore.Mvc;

namespace fyp.Areas.Customer.Controllers
{
    [Area("Customer")]

    public class PredictedPriceController : Controller
    {
        private readonly HttpClient _httpClient;
        public PredictedPriceController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }


        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("http://localhost:5000/predict"); // Update with your Flask API URL
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                ViewData["Predictions"] = content;
            }
            else
            {
                ViewData["Predictions"] = "Error retrieving data";
            }

            return View();
        }

        /*
        public IActionResult Index()
        {
            return View();
        }
        */
        /*
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("http://localhost:5000/predict"); // Update with your Flask API URL
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                ViewData["Predictions"] = content;
            }
            else
            {
                ViewData["Predictions"] = "Error retrieving data";
            }

            return View();
        }
        */
    }
}
