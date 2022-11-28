using System.Diagnostics;

namespace AsyncDemo
{
    public static class AsyncDemo
    {
        public static async Task Demo()
        {
            var tenUsdInPln = await CurrencyService.GetPriceInPln("usd", 10);

            var currencyList = new List<string>() { "usd", "eur", "jpy", "gbp", "cad" };

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            foreach (var currency in currencyList)
            {
                var result = await CurrencyService.GetPriceInPln(currency, 1);
            }
            stopwatch.Stop();
            Console.WriteLine($"1 - Elapsed Time for all is {stopwatch.ElapsedMilliseconds} ms");

            stopwatch.Reset();









            stopwatch.Start();
            var taskList = new List<Task>();
            foreach (var currency in currencyList)
            {
                taskList.Add(CurrencyService.GetPriceInPln(currency, 1));
            }
            Task.WaitAll(taskList.ToArray());
            stopwatch.Stop();
            Console.WriteLine($"2 - Elapsed Time for all is {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
