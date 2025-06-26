using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class ResultModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string File { get; set; }

    public string FilePath => $"/translated/{File}";
}
