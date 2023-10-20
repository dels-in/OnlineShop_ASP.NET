using System.Text.Json;

namespace WebApplication1.Storages;

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
        var res = new List<T>();
        foreach (var item in res)
        {
            res.Add(JsonSerializer.Deserialize<T>(fs));
        }
        return res;
    }
}