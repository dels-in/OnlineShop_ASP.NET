using System.Text;
using static System.IO.Path;
namespace WebApplication1.Models;

public static class FileStorage
{
    public static bool Exists(string fileName)
    {
        return File.Exists(Combine(Environment.CurrentDirectory, fileName));
    }
    
    public static string GetResults(string fileName)
    {
        using var textReader = new StreamReader(Combine(Environment.CurrentDirectory, fileName), Encoding.UTF8);
        return textReader.ReadToEnd();
    }
    
    public static void Clear(string fileName)
    {
        File.WriteAllText(Combine(Environment.CurrentDirectory, fileName), string.Empty);
    }
}