using System;
using System.Net.Http;
using HtmlAgilityPack;
using System.Threading.Tasks;

public class XKCDScraper
{
    private readonly HttpClient _httpClient = new HttpClient();

    public async Task RunAsync()
    {
        string url = "https://xkcd.com/1/";
        while (!string.IsNullOrEmpty(url))
        {
            try
            {
                var html = await _httpClient.GetStringAsync(url);
                var doc = new HtmlDocument();
                doc.LoadHtml(html);

                string title = doc.DocumentNode.SelectSingleNode("//div[@id='ctitle']")?.InnerText?.Trim();
                var imgNode = doc.DocumentNode.SelectSingleNode("//div[@id='comic']//img");
                string imageUrl = imgNode?.GetAttributeValue("src", "") ?? "";
                string hoverText = imgNode?.GetAttributeValue("title", "") ?? "";

                string comicNumber = GetComicNumberFromUrl(url);

                Console.WriteLine($"Comic Number: {comicNumber}");
                Console.WriteLine($"Title: {title}");
                Console.WriteLine($"Hover Text: {hoverText}");
                Console.WriteLine($"Image URL: https:{imageUrl}");
                Console.WriteLine("--------------------------------------------------");

                // Find "Next" link
                var nextLink = doc.DocumentNode.SelectSingleNode("//a[@rel='next']");
                if (nextLink == null || nextLink.GetAttributeValue("href", "#") == "#")
                    break;

                url = "https://xkcd.com" + nextLink.GetAttributeValue("href", "");
                await Task.Delay(300); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading {url}: {ex.Message}");
                break;
            }
        }

        Console.WriteLine("Finished scraping all XKCD comics!");
    }

    private string GetComicNumberFromUrl(string url)
    {
        var match = System.Text.RegularExpressions.Regex.Match(url, @"xkcd\.com/(\d+)/?");
        return match.Success ? match.Groups[1].Value : "Unknown";
    }
}
