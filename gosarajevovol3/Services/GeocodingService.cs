using Newtonsoft.Json.Linq;
namespace gosarajevovol3.Services;

public class GeocodingService
{
    private readonly HttpClient _httpClient;

    public GeocodingService()
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "GoSarajevoApp/1.0 (student.project@etf.unsa.ba)");
    }

    public async Task<(double Lat, double Lng)?> GetCoordinatesAsync(string address)
    {
        try
        {
            string fullQuery = $"{address}, Sarajevo, Bosnia and Herzegovina";
            string encodedAddress = Uri.EscapeDataString(fullQuery);
                
            string url = $"https://nominatim.openstreetmap.org/search?q={encodedAddress}&format=json&limit=1";

            string jsonResponse = await _httpClient.GetStringAsync(url);
            JArray results = JArray.Parse(jsonResponse);

            if (results.Count > 0)
            {
                double lat = Convert.ToDouble(results[0]["lat"]);
                double lon = Convert.ToDouble(results[0]["lon"]);
                return (lat, lon);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Geocoding Error]: {ex.Message}");
        }

        return null; 
    }
}