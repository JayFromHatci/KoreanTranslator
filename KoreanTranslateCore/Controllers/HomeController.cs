using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace KoreanTranslateCore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
