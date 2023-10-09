using System.Text.Json;
using WebApplication1.Models;
using static System.IO.Path;

namespace WebApplication1;

public class InMemoryFileStorage : IFileStorage
{
    public void Save<T>(List<T> list, string path)
    {
        using var fs = new FileStream(path, FileMode.OpenOrCreate);
        JsonSerializer.Serialize(fs, list, new JsonSerializerOptions { WriteIndented = true });
    }
}