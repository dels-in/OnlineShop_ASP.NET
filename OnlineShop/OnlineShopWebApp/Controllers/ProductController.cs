using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class ProductController
{
    public string Index(int id)
    {
        return string.Join("\n\n", ProductStorage.GetOneProduct(id));
    } 
}