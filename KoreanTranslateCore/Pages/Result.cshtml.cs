using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Office.Interop.Excel;
using System.IO;

namespace webTranslator.Pages;

public class ResultModel : PageModel
{
    public string? Message { get; private set; }

    public void OnGet(string message)
    {
        Message = message;
        TempData["Message"] = message; // Store the message in TempData
    }

    public IActionResult OnPostDownloadFile()
    {
        string downloadsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
        string newFilePath = Path.Combine(downloadsPath, "New Translated Excel File.xlsx");

        Application excel = new Application();
        Workbook wb = excel.Workbooks.Add();
        Worksheet ws = wb.Worksheets[1];

        // Retrieve the message from TempData
        string? message = TempData["Message"] as string;

        // Write the data from the uploaded file into the new worksheet
        if (!string.IsNullOrEmpty(message))
        {
            string[] lines = message.Split(Environment.NewLine); // Split rows
            for (int i = 0; i < lines.Length; i++)
            {
                string[] cells = lines[i].Split(" ||| "); // Split columns
                for (int j = 0; j < cells.Length; j++)
                {
                    if (!string.IsNullOrWhiteSpace(cells[j]))
                    {
                        ws.Cells[i + 1, j + 1].Value = cells[j];
                    }
                }
            }
        }

        wb.SaveAs2(newFilePath, XlFileFormat.xlOpenXMLWorkbook); // Save as .xlsx format
        wb.Close();

        TempData["Message"] = "File saved to Downloads folder.";
        return RedirectToPage();
    }

}




