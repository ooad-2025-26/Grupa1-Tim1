using gosarajevovol3.Data;
using gosarajevovol3.Models;
using gosarajevovol2.Models.Enums;
using GTranslate.Translators;

namespace gosarajevovol3.Services.Scrapers;

public class KupiKartuService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly KupiKartuScraper _scraper;

    public KupiKartuService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
        _scraper = new KupiKartuScraper(); 
    }

    private async Task<bool> CheckIfKupiKartuEverScraped()
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            bool hasEver = context.Events.Any(e => e.WebUrl.Contains("kupikartu.ba"));
            return !hasEver;
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        bool IsOk = await CheckIfKupiKartuEverScraped();
        if (IsOk)
        {
            await DoScrapingAsync();
        }
        while (!stoppingToken.IsCancellationRequested)
        {
            TimeSpan timeUntilExecution = CalculateTimeUntil1215();
            await Task.Delay(timeUntilExecution, stoppingToken);

            if (!stoppingToken.IsCancellationRequested)
            {
                await DoScrapingAsync();
            }
        }
    }

    private TimeSpan CalculateTimeUntil1215()
    {
        DateTime now = DateTime.Now;
        DateTime executionTime = now.Date.AddHours(0).AddMinutes(15);
        if (now > executionTime)
        {
            executionTime = executionTime.AddDays(1);
        }

        return executionTime.Subtract(now);
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
            Console.WriteLine($"[KupiKartu Scraper] Google limit dostignut: {ex.Message}");
        }
    }

    private EventType ChooseCategory(string url)
    {
        if (url.EndsWith("/2")) return EventType.Concert; 
        if (url.EndsWith("/3")) return EventType.Theater; 
        if (url.EndsWith("/4")) return EventType.Movie;   
        return EventType.Other; 
    }

    private async Task DoScrapingAsync()
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            List<string> kategorijeUrls = await _scraper.GetHamburgerMenuUrls();

            foreach (var katUrl in kategorijeUrls)
            {
                EventType bazniTip = ChooseCategory(katUrl);
                List<string> eventiUrls = await _scraper.GetEventUrls(katUrl);

                foreach (var url in eventiUrls)
                {
                    bool exists = context.Events.Any(e => e.WebUrl == url);
                    if (exists) continue;
                    Event? noviEvent = await _scraper.ScrapeEvents(url, bazniTip);
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
    }
    
    
}