using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System;
using Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.IO;


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

   // public IActionResult OnPost(string kText)
    public IActionResult OnPost(IFormFile uploadedFile)
    {
        string kText = string.Empty;
        if (uploadedFile != null && uploadedFile.Length > 0)
        {
            string filePath = Path.Combine(Path.GetTempPath(), uploadedFile.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                uploadedFile.CopyTo(stream);
            }

            if (System.IO.File.Exists(filePath))
            {
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                Workbook wb = excel.Workbooks.Open(filePath);
                Worksheet ws = wb.Worksheets[1];

                Microsoft.Office.Interop.Excel.Range cell = ws.Range["A1:B8"];

                foreach (var item in cell.Value)
                {
                    if (item != null)
                    {
                         kText += item.ToString() + " ";
                    }
                   

                }
                

                //write to excel



                wb.Close();
                return RedirectToPage("Result", new { message = kText.Trim() });
            }
            else
            {
                throw new FileNotFoundException("The specified file was not found.", filePath);
            }
        }
        else
        {
            // Handle the case where no file was uploaded
            return Page();
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

        Microsoft.Office.Interop.Excel.Range cellRange = ws.Range["A5"];
        cellRange.Value = "this is English";

        wb.SaveAs("C:\\Users\\R102500\\OneDrive - hatci.com\\Desktop\\Copy of WebApp Korean Test2.xlsx");
        wb.Close();

        Process.Start("C:\\Users\\R102500\\OneDrive - hatci.com\\Desktop\\Copy of WebApp Korean Test2.xlsx");
    }

}

