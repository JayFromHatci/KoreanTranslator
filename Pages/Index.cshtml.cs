using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using Microsoft.Office.Interop.Excel;
using System.Diagnostics;


namespace webTranslator.Pages;

    public class IndexModel : PageModel
{
    private string ResultMessage { get; set; } = string.Empty;

    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }

    public IActionResult OnPost(string kText)
    {
        // Process the input text (kText) here
        ResultMessage = kText; // For demonstration purposes
        return RedirectToPage("Result", new { message = ResultMessage });
    }

    /*
    private void readExcel()
    {
        string filePath = "C:\\Users\\R102500\\OneDrive - hatci.com\\Desktop\\Copy of WebApp Korean Test.xlsx";
        Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();

        Workbook wb;
        Worksheet ws;

        wb = excel.Workbooks.Open(filePath);
        ws = wb.Worksheets[1];

        Microsoft.Office.Interop.Excel.Range cell = ws.Range["A1:G8"];
        foreach (string Result in cell.Value)
        {
          
            

        }
    }

    private void writeExcel()
    {
        string filePath = "C:\\Users\\R102500\\OneDrive - hatci.com\\Desktop\\Copy of WebApp Korean Test.xlsx";
        Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();

        Workbook wb;
        Worksheet ws;

        wb = excel.Workbooks.Open(filePath);
        ws = wb.Worksheets[1];

        //Range cell = ws.Range["A1:G9"];
        Microsoft.Office.Interop.Excel.Range cellRange = ws.Range["A5"];
        cellRange.Value = "this is English";

        wb.SaveAs("C:\\Users\\R102500\\OneDrive - hatci.com\\Desktop\\Copy of WebApp Korean Test2.xlsx");
        wb.Close();

        Process.Start("C:\\Users\\R102500\\OneDrive - hatci.com\\Desktop\\Copy of WebApp Korean Test2.xlsx");
    }*/

}
