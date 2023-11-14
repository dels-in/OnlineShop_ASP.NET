namespace OnlineShopWebApp.Storages;

public interface IFileStorage
{
    void Save<T>(List<T> list, string path);
    List<T> Load<T>(string path);
}