using System.Text.Json;
using System.Text.RegularExpressions;
using gosarajevovol2.Models.Enums;
using gosarajevovol3.Models;
using HtmlAgilityPack;
namespace gosarajevovol3.Services.Scrapers;

public class NPSScraper
{
    private readonly HttpClient _httpClient = new HttpClient();
    private const string BaseUrl = "https://nps.ba/repertoar";

    public NPSScraper()
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("User-Agent",
            "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
    }

    public async Task<List<string>> GetPlayUrls()
    {
        List<string> playUrls = new List<string>();
        try
        {
            string htmlContent = await _httpClient.GetStringAsync(BaseUrl);
            var doc = new HtmlDocument();
            doc.LoadHtml(htmlContent);
            var linkNodes = doc.DocumentNode.SelectNodes("//a[contains(@href, '/repertoar/')]");
            if (linkNodes != null)
            {
                foreach (HtmlNode node in linkNodes)
                {
                    string href = node.GetAttributeValue("href", string.Empty).Trim();
                    if (href.StartsWith("/")) href = "https://nps.ba" + href;

                    if (href != "https://nps.ba/repertoar" && href != "https://nps.ba/repertoar/" &&
                        !playUrls.Contains(href))
                    {
                        playUrls.Add(href);
                    }
                }
            }
        }
        catch
        {
        }

        return playUrls;
    }

    public async Task<Event?> ScrapePlayDetails(string url)
    {
        try
        {
            string htmlContent = await _httpClient.GetStringAsync(url);
            var doc = new HtmlDocument();
            doc.LoadHtml(htmlContent);
            var root = doc.DocumentNode;

            var nameNode = root.SelectSingleNode("//h1");
            string eventName = "Unknown title";
            if (nameNode != null)
            {
                string cistoIme = nameNode.InnerText;
                cistoIme = cistoIme.Replace("Drama", "").Replace("Opera", "").Replace("Balet", "");
                eventName = HtmlEntity.DeEntitize(cistoIme).Trim();
            }

            string photoUrl = "No image";
            var posterNode =
                root.SelectSingleNode("//link[@rel='preload' and @as='image' and contains(@href, '/performance/')]")
                ?? root.SelectSingleNode("//img[contains(@src, '/performance/')]")
                ?? root.SelectSingleNode("//meta[@property='og:image']");
            if (posterNode != null)
            {
                string src = posterNode.Name switch
                {
                    "meta" => posterNode.GetAttributeValue("content", string.Empty),
                    "link" => posterNode.GetAttributeValue("href", string.Empty),
                    _ => posterNode.GetAttributeValue("src", string.Empty)
                };

                src = src.Trim();
                if (src.StartsWith("/")) photoUrl = "https://nps.ba" + src;
                else if (!src.StartsWith("http")) photoUrl = "https://nps.ba/" + src;
                else photoUrl = src;
            }

            string eventDescription = "No description";
            var textContainer = root.SelectSingleNode("//*[@id='learn_more']/..")
                                ?? root.SelectSingleNode("//article")
                                ?? root.SelectSingleNode("//div[contains(@class, 'eft')]")
                                ?? root.SelectSingleNode("//main");

            if (textContainer != null)
            {
                var pNodes = textContainer.SelectNodes(".//p");
                if (pNodes != null)
                {
                    var sb = new System.Text.StringBuilder();
                    foreach (var p in pNodes)
                    {
                        string pText = HtmlEntity.DeEntitize(p.InnerText).Trim();
                        string parentClass = p.ParentNode?.GetAttributeValue("class", "").ToLower() ?? "";
                        string pClass = p.GetAttributeValue("class", "").ToLower();

                        if (string.IsNullOrEmpty(pText) ||
                            pText == "Drama Opera Balet" ||
                            pText.Equals(eventName, StringComparison.OrdinalIgnoreCase) ||
                            pText.StartsWith("Kupi ulaznicu") ||
                            parentClass.Contains("nav") ||
                            pClass.Contains("nav") ||
                            p.GetAttributeValue("style", "").Contains("text-align: right"))
                        {
                            continue;
                        }

                        sb.Append(pText + " ");
                    }

                    string kompletanTekst = Regex.Replace(sb.ToString(), @"\s+", " ").Trim();
                    if (!string.IsNullOrEmpty(kompletanTekst))
                    {
                        eventDescription = kompletanTekst.Length > 250
                            ? kompletanTekst.Substring(0, 247) + "..."
                            : kompletanTekst;
                    }
                }
            }

            DateTime startDate = DateTime.Now;
            var scriptNodes = root.SelectNodes("//script[@type='application/ld+json']");
            if (scriptNodes != null)
            {
                foreach (var script in scriptNodes)
                {
                    try
                    {
                        using (JsonDocument jsonDoc = JsonDocument.Parse(script.InnerText))
                        {
                            JsonElement jsonRoot = jsonDoc.RootElement;
                            if (jsonRoot.TryGetProperty("startDate", out JsonElement startDateElement))
                            {
                                string? dateString = startDateElement.GetString();
                                if (!string.IsNullOrEmpty(dateString) &&
                                    DateTime.TryParse(dateString, out DateTime parsedDate))
                                {
                                    startDate = parsedDate;
                                    break;
                                }
                            }
                        }
                    }
                    catch
                    {
                    }
                }
            }

            int durationMinutes = 90;
            var allText = root.InnerText;
            var durationMatch = Regex.Match(allText, @"(\d+)\s*min");
            if (durationMatch.Success)
            {
                int.TryParse(durationMatch.Groups[1].Value, out durationMinutes);
            }

            DateTime endDate = startDate.AddMinutes(durationMinutes);

            return new Event
            {
                EventName = eventName,
                EventDescription = eventDescription,
                StartDate = startDate,
                EndDate = endDate,
                Type = EventType.Theater,
                PhotoUrl = photoUrl,
                WebUrl = url,
                LocationAddress = "National Theatre Sarajevo",
                Coordinates = new Coordinates
                {
                    Latitude = 43.85680752699714,
                    Longitude = 18.42070239669496
                }
            };
        }
        catch(Exception ex)
        { 
            return null;
        }
    }
}