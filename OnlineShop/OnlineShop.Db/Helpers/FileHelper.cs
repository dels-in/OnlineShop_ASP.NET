using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace OnlineShop.Db.Helpers;

public static class FileHelper
{
    public static void Save<T>(T[] array, string path)
    {
        var fileInfo = new FileInfo(path);
        if (fileInfo.Exists)
        {
            fileInfo.Delete();
        }

        using var fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        JsonSerializer.Serialize(fs, array, new JsonSerializerOptions { WriteIndented = true });
    }

    public static T[] Load<T>(string path)
    {
        using var fs = new FileStream(path, FileMode.OpenOrCreate);
        if (fs.Length == 0) return Array.Empty<T>();
        return JsonSerializer.Deserialize<T[]>(fs);
    }

    public static void SaveImage(IFormFile uploadedFile, string path)
    {
        using var fs = new FileStream(path, FileMode.OpenOrCreate);
        uploadedFile.CopyTo(fs);
    }
}