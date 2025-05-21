using System;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

class Program
{
    static void Main(string[] args)
    {
        int viewersCount = 1000;       
        int viewTimeSeconds = 300;     
        string url = "https://example.com";
        
        
        int maxParallelThreads = 30;
        
        var parallelOptions = new ParallelOptions
        {
            MaxDegreeOfParallelism = maxParallelThreads
        };

        Console.WriteLine($"Запуск {viewersCount} просмотров в {maxParallelThreads} потоках...");

        Parallel.For(0, viewersCount, parallelOptions, i =>
        {
            try
            {
                var options = new ChromeOptions();
                options.AddArgument("--headless");
                options.AddArgument("--disable-gpu");
                options.AddArgument("--no-sandbox");
                options.AddArgument("--disable-dev-shm-usage"); 

                using (IWebDriver driver = new ChromeDriver(options))
                {
                    driver.Navigate().GoToUrl(url);
                    Console.WriteLine($"[{i+1}]");
                    
                   
                    Thread.Sleep(viewTimeSeconds * 1000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{i+1}] Ошибка: {ex.Message}");
            }
        });

        Console.WriteLine("Все просмотры завершены.");
    }
}