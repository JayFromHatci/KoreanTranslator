using Microsoft.AspNetCore.Mvc.RazorPages;

namespace webTranslator.Pages;

public class ResultModel : PageModel
{
    public string Message { get; private set; }

    public void OnGet(string message)
    {
        Message = message;
    }
}