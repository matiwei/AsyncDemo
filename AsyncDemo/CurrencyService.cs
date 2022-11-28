using System.Diagnostics;
using System.Text.Json;

namespace AsyncDemo
{
    public static class CurrencyService
    {
        public static async Task<decimal> GetPriceInPln(string currency, decimal amount)
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            var client = new HttpClient();
            var response = await client.GetAsync($"https://api.nbp.pl/api/exchangerates/rates/a/{currency}/last/1/?format=json");
            var content = await response.Content.ReadAsStringAsync();
            var deserialized = JsonSerializer.Deserialize<NbpRateResponse>(content);

            if (deserialized == null) throw new ArgumentNullException();
            stopwatch.Stop();

            Console.WriteLine($"Elapsed Time for {currency} is {stopwatch.ElapsedMilliseconds} ms");
            return deserialized.rates.First().mid * amount;
        }

    }

    public class NbpRateResponse
    {
        public string table { get; set; }
        public string currency { get; set; }
        public string code { get; set; }
        public Rate[] rates { get; set; }
    }

    public class Rate
    {
        public string no { get; set; }
        public string effectiveDate { get; set; }
        public decimal mid { get; set; }
    }
}
