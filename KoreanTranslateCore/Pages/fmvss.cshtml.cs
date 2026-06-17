using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Office.Interop.Excel;
using static System.Runtime.InteropServices.JavaScript.JSType;




namespace webTranslator.Pages
{

    public class fmvssModel : PageModel
    {
        
        public const double PI = 3.1415926535897931;



        [BindProperty]
        public double di1 { get; set; }
        [BindProperty]
        public double di2 { get; set; }
        [BindProperty]
        public double di3 { get; set; }

        [BindProperty]
        public double thetaA { get; set; }
        [BindProperty]
        public double thetaXA { get; set; }
        [BindProperty]
        public double thetaXXA { get; set; }
        [BindProperty]
        public double thetaXXXA { get; set; }
        [BindProperty]
        public double minuteA { get; set; }
        [BindProperty]
        public double secondA { get; set; }

        [BindProperty]
        public double thetaB { get; set; }
        [BindProperty]
        public double thetaXB { get; set; }
        [BindProperty]
        public double thetaXXB { get; set; }
        [BindProperty]
        public double thetaXXXB { get; set; }
        [BindProperty]
        public double minuteB { get; set; }
        [BindProperty]
        public double secondB { get; set; }

        [BindProperty]
        public double thetaC { get; set; }
        [BindProperty]
        public double thetaXC { get; set; }
        [BindProperty]
        public double thetaXXC { get; set; }
        [BindProperty]
        public double thetaXXXC { get; set; }
        [BindProperty]
        public double minuteC { get; set; }
        [BindProperty]
        public double secondC { get; set; }

        [BindProperty]
        public double thetaAvg { get; set; }
        [BindProperty]
        public double thetaXAvg { get; set; }
        [BindProperty]
        public double thetaXXAvg { get; set; }
        [BindProperty]
        public double thetaXXXAvg { get; set; }
        [BindProperty]
        public double minuteAvg { get; set; }
        [BindProperty]
        public double secondAvg { get; set; }

        [BindProperty]
        public double d { get; set; }


        [BindProperty]
        public double degA { get; set; }
        [BindProperty]
        public double degB { get; set; }
        [BindProperty]
        public double degC { get; set; }
        [BindProperty]
        public double degAvg { get; set; }


        [BindProperty]
        public double CylA { get; set; }
        [BindProperty]
        public double CylB { get; set; }
        [BindProperty]
        public double CylC { get; set; }
        [BindProperty]
        public double Sscale { get; set; }
        [BindProperty]
        public double Aeye { get; set; }




        public void OnGet()
        {
        }



        public IActionResult OnPost()
        {
                di1 = CylA;
                di2 = CylB;
                di3 = CylC;
                d = Sscale * Aeye;

                thetaA = Math.Asin(di1 / d) * (180 / PI);
                thetaB = Math.Asin(di2 / d) * (180 / PI);
                thetaC = Math.Asin(di3 / d) * (180 / PI);

                degA = Math.Truncate(thetaA);
                degB = Math.Truncate(thetaB);
                degC = Math.Truncate(thetaC);

                thetaXA = (thetaA - Math.Truncate(thetaA)) * 60;       //minute value with decimal
                thetaXB = (thetaB - Math.Truncate(thetaB)) * 60;       //minute value with decimal
                thetaXC = (thetaC - Math.Truncate(thetaC)) * 60;       //minute value with decimal

                minuteA = Math.Truncate(thetaXA);                     //whole number-> arc minute
                minuteB = Math.Truncate(thetaXB);                     //whole number-> arc minute
                minuteC = Math.Truncate(thetaXC);                     //whole number-> arc minute

                thetaXXA = thetaXA - minuteA;                          //decimal value of arc second  
                thetaXXB = thetaXB - minuteB;                          //decimal value of arc second
                thetaXXC = thetaXC - minuteC;                          //decimal value of arc second

                secondA = thetaXXA * 60;
                secondB = thetaXXB * 60;
                secondC = thetaXXC * 60;                              //Arc second

                thetaAvg = (thetaA + thetaB + thetaC) / 3;
                degAvg = Math.Truncate(thetaAvg);
                thetaXAvg = (thetaAvg - Math.Truncate(thetaAvg)) * 60;       //minute value with decimal
                minuteAvg = Math.Truncate(thetaXAvg);                     //whole number-> arc minute
                thetaXXAvg = thetaXAvg - minuteAvg;                          //decimal value of arc second
                secondAvg = thetaXXAvg * 60;                              //Arc second



                return RedirectToPage("fmvssResult", new { ArcDegreesA = degA, ArcMinutesA = minuteA, ArcSecondsA = secondA, ArcDegreesB = degB, ArcMinutesB = minuteB, ArcSecondsB = secondB, ArcDegreesC = degC, ArcMinutesC = minuteC, ArcSecondsC = secondC, ArcDegreesAvg = degAvg, ArcMinutesAvg = minuteAvg, ArcSecondsAvg = secondAvg });



        }


    }
}
