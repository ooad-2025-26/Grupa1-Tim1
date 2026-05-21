using Microsoft.AspNetCore.Mvc;
using static Azure.Core.HttpHeader;

namespace gosarajevol3.Controllers
{
    public class InformationController : Controllergit checkout --theirs gosarajevovol3.sln
git checkout --theirs gosarajevovol3/wwwroot/css/site.css
git add.
git commit -m "Merge home-npsScraper into develop"git checkout --theirs gosarajevovol3.sln
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Schedule(string type, string line)
        {
            ViewBag.Type = type;
            ViewBag.Line = line;

            return View();
        }
    }
}