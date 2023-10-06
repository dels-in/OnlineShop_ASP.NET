using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Models;

public class SearchModel : PageModel
{
    public string Message { get; private set; } = "";

    public void OnGet() => Message = "Введите свое имя";
         
    public void OnPost(string productName) => Message = productName;
}