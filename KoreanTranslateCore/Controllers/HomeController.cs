using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Encodings.Web;
using System.Linq;

namespace KoreanTranslateCore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {           
            return View();
        }
        /*
         * Create a method here to extract file directory and pass to TranslateController
         * Maybe TranslateModel will handle the excel data?
         */
    }
}
