using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class ProductController
{
    public string Index(int id)
    {
        var answer = string.Join("\n\n", ProductStorage.GetOneProduct(id)) ?? 
                     "Продукта с таким ID не существует";
        return answer;
    } 
}