using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using WebScapperAPI.Modules;
using WebScapperAPI.Data;
using WebScapperAPI.Models;

public class SearchEngineService
{

    private readonly EngineResultDbContext _dbContext;

    public SearchEngineService(EngineResultDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<EngineResultDto> PerformEngineSearch(string keyword, string url, string engineNames)
    {
        var results = new List<EngineResultDto>();
        string[] arrEngines = engineNames.Split(',');
        foreach (var engineName in arrEngines)
        {
            if (engineName == "google")
            {
                string ranking = PerformGoogleSearch(keyword, url);
                results.Add(this.createEngineResult(keyword, url, engineName, ranking));
            }
            else if (engineName == "bing")
            {
                string ranking = PerformBingSearch(keyword, url);
                results.Add(this.createEngineResult(keyword, url, engineName, ranking));
            }
        }

        this.saveResultHistory(results);

        return results;
    }

    private string PerformGoogleSearch(string keyword, string url)
    { 

        using (IWebDriver driver = new ChromeDriver(this.createChromeBrowserOptions()))
        {
            driver.Url = $"https://www.google.com/search?num=100&q={Uri.EscapeDataString(keyword)}";

            // Wait for the search results to load
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(driver => driver.FindElement(By.CssSelector("div#search")));

            var searchResults = driver.FindElements(By.CssSelector("div.tF2Cxc"));

            List<int> positions = new List<int>();

            for (int i = 0; i < searchResults.Count; i++)
            {
                var linkElement = searchResults[i].FindElement(By.CssSelector("a"));
                var link = linkElement.GetAttribute("href");
                if (link.Contains(url))
                {
                    positions.Add(i + 1);
                }
            }

            if (positions.Any())
            {
                return string.Join(", ", positions);
            }

            return "0";
        }
    }

    private string PerformBingSearch(string keyword, string url)
    {
        using (IWebDriver driver = new ChromeDriver(this.createChromeBrowserOptions()))
        {
            driver.Url = $"https://www.bing.com/search?num=100&q={Uri.EscapeDataString(keyword)}";
            List<int> positions = new List<int>();

            for (int pageNumber = 1; pageNumber <= 10; pageNumber++) // Assuming 10 pages with 10 results each
            {
                // Wait for the search results to load
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(driver => driver.FindElement(By.CssSelector("ol#b_results")));

                var searchResults = driver.FindElements(By.CssSelector("li.b_algo"));

                for (int i = 0; i < searchResults.Count; i++)
                {
                    var linkElement = searchResults[i].FindElement(By.CssSelector("h2 a"));
                    var link = linkElement.GetAttribute("href");
                    if (link.Contains(url))
                    {
                        positions.Add((pageNumber - 1) * 10 + i + 1);
                    }
                }

                // If there are enough positions, break the loop
                if (positions.Count >= 100)
                {
                    break;
                }

                // Navigate to the next page if available
                try
                {
                    var nextButton = driver.FindElement(By.CssSelector("a.sb_pagN"));
                    Actions actions = new Actions(driver);
                    actions.MoveToElement(nextButton).Click().Perform();
                }
                catch (NoSuchElementException)
                {
                    // Break the loop if there is no next page
                    break;
                }
            }

            if (positions.Any())
            {
                return string.Join(", ", positions);
            }

            return "0";
        }
    }

    private void saveResultHistory(List<EngineResultDto> searchResults)
    {
        foreach (var result in searchResults)
        {
            var engineResult = new EngineResult
            {
                KeyWords = result.KeyWords,
                Url = result.Url,
                EngineName = result.EngineName,
                Ranking = result.Ranking,
                TimeStamp = result.TimeStamp
            };

            _dbContext.EngineResults.Add(engineResult);
        }

        _dbContext.SaveChanges();
    }

    private EngineResultDto createEngineResult(string keyword, string url, string engineName, string ranking)
    {
        return new EngineResultDto
        {
            KeyWords = keyword,
            Url = url,
            EngineName = engineName,
            Ranking = ranking,
            TimeStamp = DateTime.UtcNow
        };
    }

    private ChromeOptions createChromeBrowserOptions()
    {
        var chromeOptions = new ChromeOptions();
        chromeOptions.AddArgument("--headless"); // Run Chrome in headless mode (without a visible browser window)
        chromeOptions.AddArgument("--disable-gpu"); // Disable GPU to avoid potential issues
        return chromeOptions;
    }
}