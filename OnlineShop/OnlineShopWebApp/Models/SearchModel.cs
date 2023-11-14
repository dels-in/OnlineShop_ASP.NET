using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OnlineShopWebApp.Models;

public class SearchModel : PageModel
{
    public string Message { get; private set; } = "";
    public void OnPost(string searchChars) => Message = searchChars;
}