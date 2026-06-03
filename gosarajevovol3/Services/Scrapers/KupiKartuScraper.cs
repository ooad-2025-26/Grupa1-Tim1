using HtmlAgilityPack;
using System.Globalization;
using System.Text;
using gosarajevovol2.Models.Enums;
using gosarajevovol3.Models;

namespace gosarajevovol3.Services.Scrapers;

public class KupiKartuScraper
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "https://www.kupikartu.ba";
    private readonly GeocodingService _geocodingService = new GeocodingService();
    
    public KupiKartuScraper()
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
    }
    
    public async Task<List<string>> GetHamburgerMenuUrls()
    {
        List<string> hamburgerUrls = new List<string>();

        try
        {
            string htmlContent = await _httpClient.GetStringAsync(BaseUrl);
            var doc = new HtmlDocument();
            doc.LoadHtml(htmlContent);
            var linkNodes = doc.DocumentNode.SelectNodes("//a[contains(@href, '/karte/kategorija/')]");

            if (linkNodes != null)
            {
                foreach (HtmlNode node in linkNodes)
                {
                    string href = node.GetAttributeValue("href", string.Empty).Trim();
                    if (href.StartsWith("/"))
                    {
                        href = BaseUrl +  href;
                    }
                    if (!hamburgerUrls.Contains(href)) 
                    {
                        hamburgerUrls.Add(href);
                    }
                }
            }
        }
        catch(Exception ex)
        {
        }
        return hamburgerUrls;
    } 
    
    public async Task<List<string>> GetEventUrls(string url)
    {
        List<string> eventUrls = new List<string>();
        try
        {
            string htmlContent = await _httpClient.GetStringAsync(url);
            var doc = new HtmlDocument();
            doc.LoadHtml(htmlContent);
            var linkNodes = doc.DocumentNode.SelectNodes("//a[contains(@href, '/karte/event/')]");
            if (linkNodes != null)
            {
                foreach (HtmlNode node in linkNodes)
                {
                    string href = node.GetAttributeValue("href", string.Empty).Trim();
                    if (href.StartsWith("/"))
                    {
                        href = BaseUrl +  href;
                    }
                    if (!eventUrls.Contains(href)) 
                    {
                        eventUrls.Add(href);
                    }
                }
            }
        }
        catch(Exception ex)
        {
        }
        return eventUrls;
    }
    
    static EventType ChooseCategory(string url)
    {
        if (url.EndsWith("/2")) return EventType.Concert; 
        if (url.EndsWith("/3")) return EventType.Theater; 
        if (url.EndsWith("/4")) return EventType.Movie;   
        return EventType.Other; 
    }

    public async Task<Event?> ScrapeEvents(string url, EventType eventType)
    {
        try
        {
            string htmlContent = await _httpClient.GetStringAsync(url);
            var doc = new HtmlDocument();
            doc.LoadHtml(htmlContent);

            var root = doc.DocumentNode;
            var loactionNode = doc.DocumentNode.SelectSingleNode("(//body//p)[2]");
            if (loactionNode != null)
            {
                string locationText = loactionNode.InnerText.Trim();
                if (locationText.Contains("Sarajevo", StringComparison.OrdinalIgnoreCase))
                {
                    var nameNode = doc.DocumentNode.SelectSingleNode("(//body//h1)[1]");
                    string name = "Name Unknown";
                    if (nameNode != null)
                    {
                        name = HtmlEntity.DeEntitize(nameNode.InnerText).Trim();
                    }

                    var imgNode = doc.DocumentNode.SelectSingleNode("//div[@class='image']//img");
                    string imgUrl = string.Empty;
                    if (imgNode != null)
                    {
                        imgUrl = imgNode.GetAttributeValue("src", string.Empty).Trim();
                        if (imgUrl.StartsWith("/"))
                        {
                            imgUrl = BaseUrl + imgUrl;
                        }
                    }
                    else
                    {
                        var altImgNode = doc.DocumentNode.SelectSingleNode("//img[@class='image']");
                        if (altImgNode != null)
                        {
                            imgUrl = altImgNode.GetAttributeValue("src", string.Empty).Trim();
                            if (imgUrl.StartsWith("/")) imgUrl = BaseUrl + imgUrl;
                        }
                    }

                    DateTime startDate = DateTime.Today;
                    var startDateNode = doc.DocumentNode.SelectSingleNode("(//body//p)[1]/text()");

                    if (startDateNode != null)
                    {
                        string startDateText = HtmlEntity.DeEntitize(startDateNode.InnerText).Trim();
                        if (startDateText.Contains("-"))
                        {
                            try
                            {
                                var parts = startDateText.Split('-');
                                var rightSideParts = parts[1].Split('/');

                                if (rightSideParts.Length > 1)
                                {
                                    string dan = parts[0].Trim();
                                    string mjesec = rightSideParts[1].Trim();
                                    startDateText = $"{dan}/{mjesec}";
                                }
                            }
                            catch
                            {
                            }
                        }

                        string completeDate = $"{startDateText}/{DateTime.Now.Year}";
                        if (DateTime.TryParseExact(completeDate, "dd/MM/yyyy",
                                CultureInfo.InvariantCulture,
                                DateTimeStyles.None,
                                out DateTime parsedDate))
                        {
                            startDate = parsedDate;
                        }
                    }

                    var locNode = doc.DocumentNode.SelectSingleNode("(//body//p)[2]/text()");
                    string address = "";
                    if (locNode != null)
                    {
                        string wholeText = HtmlEntity.DeEntitize(locNode.InnerText).Trim();
                        if (wholeText.Contains("Sarajevo", StringComparison.OrdinalIgnoreCase))
                        {
                            string wholeAddressText = wholeText
                                .Replace(", Sarajevo", "", StringComparison.OrdinalIgnoreCase)
                                .Replace(",Sarajevo", "", StringComparison.OrdinalIgnoreCase)
                                .Replace("Sarajevo", "", StringComparison.OrdinalIgnoreCase)
                                .Trim();
                            address = wholeAddressText;
                        }
                    }

                    EventType finalType = eventType;
                    if (finalType == EventType.Concert && name.Contains("Festival", StringComparison.OrdinalIgnoreCase))
                    {
                        finalType = EventType.Festival;
                    }

                    StringBuilder descriptionBuilder = new StringBuilder();
                    var descriptionNodes =
                        doc.DocumentNode.SelectNodes("//body//p[@class='MsoNormal' or @class='MsoNoSpacing']");

                    if (descriptionNodes != null)
                    {
                        foreach (var node in descriptionNodes)
                        {
                            string tekstParagrafa = HtmlEntity.DeEntitize(node.InnerText).Trim();
                            if (!string.IsNullOrEmpty(tekstParagrafa))
                            {
                                descriptionBuilder.AppendLine(tekstParagrafa);
                            }
                        }
                    }

                    double lat = 43.8563;
                    double lon = 18.4131;

                    if (!string.IsNullOrEmpty(address))
                    {
                        var coordinates = await _geocodingService.GetCoordinatesAsync(address);
                        if (coordinates != null)
                        {
                            lat = coordinates.Value.Lat;
                            lon = coordinates.Value.Lng;
                        }
                        else
                        {
                            var words = address.Split(new[] { ' ', '-', ',' }, StringSplitOptions.RemoveEmptyEntries);
                            if (words.Length > 1)
                            {
                                string shortAddress = $"{words[0]} {words[1]}";
                                var fallbackCoords = await _geocodingService.GetCoordinatesAsync(shortAddress);
                                if (fallbackCoords != null)
                                {
                                    lat = fallbackCoords.Value.Lat;
                                    lon = fallbackCoords.Value.Lng;
                                }
                            }
                        }
                    }

                    string eventDescription = descriptionBuilder.ToString().Trim();

                    var newEvent = new Event
                    {
                        EventName = name,
                        EventDescription = eventDescription,
                        StartDate = startDate,
                        EndDate = startDate, 
                        Type = finalType,
                        PhotoUrl = imgUrl,
                        WebUrl = url,
                        LocationAddress = address,
                        Coordinates = new Coordinates
                        {
                            Latitude = lat,
                            Longitude = lon
                        }
                    };
                    return newEvent;
                }
            }
        }
        catch (Exception ex)
        {
        }
        return null;
    }
}