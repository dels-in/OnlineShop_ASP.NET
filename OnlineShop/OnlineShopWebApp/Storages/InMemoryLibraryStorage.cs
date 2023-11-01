using WebApplication1.Areas.Admin.Models;
using WebApplication1.Models;

namespace WebApplication1.Storages;

public class InMemoryLibraryStorage : IStorage<Library, Product>
{
    private List<Library> _library = new();

    public void AddToList(Product product, string userId)
    {
        var library = GetByUserId(userId);
        if (library == null)
        {
            _library.Add(new Library
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Products = new() { product }
            });
        }
        else
        {
            var libraryItem = library.Products.FirstOrDefault(ci => ci.Id == product.Id);
            if (libraryItem == null)
            {
                library.Products.Add(product);
            }
        }
    }

    public void AddToList(Product checkout, Cart cart, string userId)
    {
        throw new NotImplementedException();
    }

    public void Delete(Product product, string userId)
    {
        var library = GetByUserId(userId);
        if (library != null)
        {
            var libraryItem = library.Products.FirstOrDefault(ci => ci.Id == product.Id);
            if (libraryItem != null)
            {
                _library.FirstOrDefault(li => li == library).Products.Remove(libraryItem);
            }
        }
    }

    public Library GetByUserId(string userId)
    {
        return _library.FirstOrDefault(c => c.UserId == userId);
    }

    public List<Library> GetAll()
    {
        return _library;
    }

    public void Clear(Library parameter)
    {
        throw new NotImplementedException();
    }

    public void Reduce(Product product, string userId)
    {
        throw new NotImplementedException();
    }

    public void Edit(Guid id, OrderStatus status)
    {
        throw new NotImplementedException();
    }
}