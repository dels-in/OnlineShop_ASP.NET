using System.Text.Json;
using WebApplication1.Models;
using static System.IO.Path;

namespace WebApplication1;

public class InMemoryFileStorage : IFileStorage
{
    public void Save<T>(List<T> list, string path)
    {
        using var fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read, 4096, FileOptions.DeleteOnClose);
        JsonSerializer.Serialize(fs, list, new JsonSerializerOptions { WriteIndented = true });
    }
    
    public List<T> Load<T>(string path)
    {
        using var fs = new FileStream(path, FileMode.OpenOrCreate);
        if (fs.Length == 0) return new List<T>();
        return JsonSerializer.Deserialize<List<T>>(fs);
    }
}