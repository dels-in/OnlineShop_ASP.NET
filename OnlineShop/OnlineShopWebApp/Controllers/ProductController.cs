using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class ProductController
{
    public string Index(int id)
    {
        var productId = ProductStorage.GetOneProduct(id);
        if (productId == null)
            return "Продукта с таким ID не существует.";
        return productId.ToString();
    } 
}