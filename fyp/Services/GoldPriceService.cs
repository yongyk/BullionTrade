using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace fyp.Services
{
    public class GoldPriceService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "LEW5WUUEX0P90ID9";
        public GoldPriceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        /*
        public async Task<decimal> GetGoldPriceAsync()
        {
           // string QUERY_URL = $"https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol=GLD&interval=1min&outputsize=full&apikey=LEW5WUUEX0P90ID9";

            string QUERY_URL = "https://www.alphavantage.co/query?function=COPPER&interval=monthly&apikey=LEW5WUUEX0P90ID9";
            //string QUERY_URL = "https://www.alphavantage.co/query?function=CURRENCY_EXCHANGE_RATE&from_currency=BTC&to_currency=EUR&apikey=demo";           // string apiUrl = "https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol=IBM&apikey=demo";
           // string apiUrl = $"https://www.alphavantage.co/query?function=CURRENCY_EXCHANGE_RATE&from_currency=GLD&to_currency=USD&apikey=LEW5WUUEX0P90ID9";
            var response = await _httpClient.GetAsync(QUERY_URL);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var jsonDocument = JsonDocument.Parse(content);
                  var price = jsonDocument.RootElement.GetProperty("Realtime Currency Exchange Rate").GetProperty("5. Exchange Rate").GetString();
                 return decimal.Parse(price);
            }
            return 0;  // Return default value or consider throwing a custom exception if necessary
        }

    }
        */

        public async Task<Dictionary<DateTime, decimal>> GetGoldPriceIntradayAsync()
        {
            string url = $"https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol=GLD&interval=1min&outputsize=full&apikey=LEW5WUUEX0P90ID9";
            var response = await _httpClient.GetAsync(url);
            var pricePoints = new Dictionary<DateTime, decimal>();

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                using var jsonDocument = JsonDocument.Parse(content);
                var root = jsonDocument.RootElement;
                var timeSeries = root.GetProperty("Time Series (1min)");

                foreach (var element in timeSeries.EnumerateObject())
                {
                    var dateTime = DateTime.Parse(element.Name);
                    var openPrice = decimal.Parse(element.Value.GetProperty("1. open").GetString());
                    pricePoints.Add(dateTime, openPrice);  // Assuming you want the open price
                }
            }
            return pricePoints;
        }

    }
}
