using System.Text.Json;
using WebApplication1.Models;
using static System.IO.Path;

namespace WebApplication1;

public class FileStorage
{
    public void SaveProducts(List<Product> products)
    {
        using var fs = new FileStream("Products.json", FileMode.OpenOrCreate);
        JsonSerializer.Serialize(fs, products, new JsonSerializerOptions { WriteIndented = true });
    }
}