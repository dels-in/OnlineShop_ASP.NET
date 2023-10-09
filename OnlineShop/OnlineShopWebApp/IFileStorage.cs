using WebApplication1.Models;

namespace WebApplication1;

public interface IFileStorage
{
    void Save<T>(List<T> list, string path);
}