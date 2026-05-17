using Microsoft.AspNetCore.Mvc;

namespace gosarajevol3.Controllers
{
    public class InformationController : Controller
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