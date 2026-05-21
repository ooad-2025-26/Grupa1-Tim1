
using gosarajevovol3.Data;
using gosarajevovol3.Models;

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
                    context.Events.Add(noviEvent);
                }

                await Task.Delay(1500);
            }

            await context.SaveChangesAsync();
        }
    }
}