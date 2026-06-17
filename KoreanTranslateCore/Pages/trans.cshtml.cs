using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Threading.Tasks;
using DeepL;
using System;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;
using A = DocumentFormat.OpenXml.Drawing;

public class TransModel : PageModel
{
    private readonly IWebHostEnvironment _environment;

    public TransModel(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    // Renamed to avoid conflict with PageModel.File()
    [BindProperty]
    public IFormFile UploadedFile { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (UploadedFile == null || UploadedFile.Length == 0)
        {
            ModelState.AddModelError(string.Empty, "Please upload a valid file.");
            return Page();
        }

        var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
        var translatedFolder = Path.Combine(_environment.WebRootPath, "translated");

        Directory.CreateDirectory(uploadsFolder);
        Directory.CreateDirectory(translatedFolder);

        var originalPath = Path.Combine(uploadsFolder, UploadedFile.FileName);
        var translatedFileName = $"translated_{UploadedFile.FileName}";
        var translatedPath = Path.Combine(translatedFolder, translatedFileName);

        // Save uploaded file
        using (var stream = new FileStream(originalPath, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            await UploadedFile.CopyToAsync(stream);
        }

        try
        {
            var authKey = "33f0b74f-bbb4-4184-9f5b-3f1a0d5adecc:fx"; // your key

            var client = new DeepLClient(authKey);

            if (System.IO.File.Exists(translatedPath))
                System.IO.File.Delete(translatedPath);

            var ext = Path.GetExtension(originalPath).ToLowerInvariant();

            if (ext == ".pptx")
            {
                // Copy original PPTX to translated path first
                System.IO.File.Copy(originalPath, translatedPath, true);


                // Open copied file for editing
                using (var ppt = PresentationDocument.Open(translatedPath, true))
                {
                    foreach (var slidePart in ppt.PresentationPart.SlideParts)
                    {
                        var texts = slidePart.Slide.Descendants<A.Text>();
                        foreach (var text in texts)
                        {
                            if (!string.IsNullOrWhiteSpace(text.Text))
                            {
                                var result = await client.TranslateTextAsync(text.Text, null, "EN-US");
                                text.Text = result.Text;
                            }
                        }
                    }
                }
            }
            else
            {
                // Other files: translate normally
                await client.TranslateDocumentAsync(
                    new FileInfo(originalPath),
                    new FileInfo(translatedPath),
                    null,   // auto-detect source
                    "EN-US" // target
                );
            }

            // Return translated file as download
            byte[] fileBytes = System.IO.File.ReadAllBytes(translatedPath);

            // Determine MIME type
            string mimeType = ext switch
            {
                ".pptx" => "application/vnd.openxmlformats-officedocument.presentationml.presentation",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                ".pdf" => "application/pdf",
                ".txt" => "text/plain",
                ".html" => "text/html",
                _ => "application/octet-stream"
            };

            return File(fileBytes, mimeType, translatedFileName);
        }
        catch (DocumentTranslationException ex)
        {
            var message = ex.Message;
            if (ex.DocumentHandle != null)
                message += $" (Doc ID: {ex.DocumentHandle.Value.DocumentId})";

            ModelState.AddModelError(string.Empty, message);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Unexpected error: {ex.Message}");
        }

        return Page();
    }
}
