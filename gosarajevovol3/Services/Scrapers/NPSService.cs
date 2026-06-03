
using gosarajevovol3.Data;
using gosarajevovol3.Models;
using GTranslate.Translators;

namespace gosarajevovol3.Services.Scrapers;

public class NPSService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly NPSScraper _scraper;
    public NPSService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
        _scraper = new NPSScraper(); 
    }

    private async Task<bool> CheckIfNpsEverScraped()
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var context =  scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            bool hasEver = context.Events.Any(e => e.WebUrl.Contains("nps.ba"));
            return !hasEver;
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        bool IsOk = await CheckIfNpsEverScraped();
        if (IsOk)
        {
            await DoScrapingAsync();
        }

        while (!stoppingToken.IsCancellationRequested)
        {
            TimeSpan timeUntilMidnight = CalculateTimeUntilMidnight();
            await Task.Delay(timeUntilMidnight, stoppingToken);

            if (!stoppingToken.IsCancellationRequested)
            {
                await DoScrapingAsync();
            }
        }
    }
    private TimeSpan CalculateTimeUntilMidnight()
    {
        DateTime now = DateTime.Now;
        DateTime midnight = now.Date.AddDays(1);
        return midnight.Subtract(now);
    }
    
    private async Task DoScrapingAsync()
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            List<string> urls = await _scraper.GetPlayUrls();

            foreach (var url in urls)
            {
                bool exists = context.Events.Any(e => e.WebUrl == url);
                if (exists) continue;

                Event? noviEvent = await _scraper.ScrapePlayDetails(url);
                if (noviEvent != null)
                {
                    await TranslateInEnAsync(noviEvent);

                    context.Events.Add(noviEvent);
                    await context.SaveChangesAsync();
                }
                await Task.Delay(2500);
            }
        }
    }
    
    private async Task TranslateInEnAsync(Event ev)
    {
        var translator = new GoogleTranslator();
        try
        {
            if (!string.IsNullOrEmpty(ev.EventName))
            {
                var nameTrans = await translator.TranslateAsync(ev.EventName, "en", "bs");
                ev.EventNameEn = nameTrans.Translation;
                await Task.Delay(800);
            }
            if (!string.IsNullOrEmpty(ev.EventDescription))
            {
                var descTrans = await translator.TranslateAsync(ev.EventDescription, "en", "bs");
                ev.EventDescriptionEn = descTrans.Translation;
                await Task.Delay(800);
            }
            if (!string.IsNullOrEmpty(ev.LocationAddress))
            {
                var locTrans = await translator.TranslateAsync(ev.LocationAddress, "en", "bs");
                ev.LocationAddressEn = locTrans.Translation;
            }
        }
        catch (Exception ex)
        {
            ev.EventNameEn = ev.EventName;
            ev.EventDescriptionEn = ev.EventDescription;
            ev.LocationAddressEn = ev.LocationAddress;
            Console.WriteLine($"[NPS Scraper] Google limit dostignut: {ex.Message}");
        }
    }
}