using System.Text.Json;
using WebApplication1.Models;
using static System.IO.Path;

namespace WebApplication1;

public class FileStorage
{
    public static bool Exists(string fileName)
    {
        return File.Exists(Combine(Environment.CurrentDirectory, fileName));
    }

    public void SaveProducts(List<Product> products)
    {
        using var fs = new FileStream("Products.json", FileMode.OpenOrCreate);
        JsonSerializer.Serialize(fs, products, new JsonSerializerOptions { WriteIndented = true });
    }
}