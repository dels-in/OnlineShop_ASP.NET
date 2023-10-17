namespace WebApplication1.Storages;

public interface IStorage<T, K>
{
    void AddToList(K parameter, string userId);
    void Delete(K parameter, string userId);
    void Reduce(K parameter, string userId);
    T GetByUserId(string userId);
}