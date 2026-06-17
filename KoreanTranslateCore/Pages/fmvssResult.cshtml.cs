using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace webTranslator.Pages
{
    public class fmvssResultModel : PageModel
    {

        [BindProperty(SupportsGet = true)]
        public double ArcDegreesA { get; set; }
        [BindProperty(SupportsGet = true)]
        public double ArcMinutesA { get; set; }
        [BindProperty(SupportsGet = true)]
        public double ArcSecondsA { get; set; }

        [BindProperty(SupportsGet = true)]
        public double ArcDegreesB { get; set; }
        [BindProperty(SupportsGet = true)]
        public double ArcMinutesB { get; set; }
        [BindProperty(SupportsGet = true)]
        public double ArcSecondsB { get; set; }

        [BindProperty(SupportsGet = true)]
        public double ArcDegreesC { get; set; }
        [BindProperty(SupportsGet = true)]
        public double ArcMinutesC { get; set; }
        [BindProperty(SupportsGet = true)]
        public double ArcSecondsC { get; set; }


        [BindProperty(SupportsGet = true)]
        public double ArcDegreesAvg { get; set; }
        [BindProperty(SupportsGet = true)]
        public double ArcMinutesAvg { get; set; }
        [BindProperty(SupportsGet = true)]
        public double ArcSecondsAvg { get; set; }





        public void OnGet()
            {
               
            }

        public IActionResult OnPost()
        {
            // Redirect to the same page to display the results
            return RedirectToPage("fmvss");
        }
        


    }
}
