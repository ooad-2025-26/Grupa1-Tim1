using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using gosarajevovol3.Models;

namespace gosarajevovol3.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var videos = new List<string>
        {
            "/Videos/videoHome1.mp4",
            "/Videos/videoHome2.mp4",
            "/Videos/videoHome3.mp4"
        };
        var random =  new Random();
        int index = random.Next(videos.Count);
        ViewBag.SelectedVideo = videos[index];
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}