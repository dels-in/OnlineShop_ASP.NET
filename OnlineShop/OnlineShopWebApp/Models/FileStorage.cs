using System.Text.Json;
using static System.IO.Path;

namespace WebApplication1.Models;

public static class FileStorage
{
    public static bool Exists(string fileName)
    {
        return File.Exists(Combine(Environment.CurrentDirectory, fileName));
    }

    public static void SaveProducts(List<Product> products)
    {
        using var fs = new FileStream("Products.json", FileMode.OpenOrCreate);
        JsonSerializer.Serialize(fs, products, new JsonSerializerOptions { WriteIndented = true });
    }
}