namespace WebApplication1.Storages;

public interface IFileStorage
{
    void Save<T>(List<T> list, string path);
    List<T> Load<T>(string path);
}