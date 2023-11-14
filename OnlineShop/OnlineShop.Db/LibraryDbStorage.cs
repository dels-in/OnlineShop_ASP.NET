using OnlineShop.Db.Models;

namespace OnlineShop.Db;

public class LibraryDbStorage : IStorage<Library, Product>
{
    private readonly DatabaseContext _dbContext;

    public LibraryDbStorage(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void AddToList(Product product, string userId)
    {
        var library = GetByUserId(userId);
        if (library == null)
        {
            _dbContext.Libraries.Add(new Library
            {
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

        _dbContext.SaveChanges();
    }

    public void Delete(Product product, string userId)
    {
        var library = GetByUserId(userId);
        if (library != null)
        {
            var libraryItem = library.Products.FirstOrDefault(ci => ci.Id == product.Id);
            if (libraryItem != null)
            {
                _dbContext.Libraries.FirstOrDefault(li => li == library).Products.Remove(libraryItem);
            }
        }

        _dbContext.SaveChanges();
    }

    public Library GetByUserId(string userId)
    {
        return _dbContext.Libraries.FirstOrDefault(c => c.UserId == userId);
    }

    public List<Library> GetAll()
    {
        return _dbContext.Libraries.ToList();
    }

    public void Clear(Library parameter)
    {
        throw new NotImplementedException();
    }

    public void AddToList(Product checkout, Cart cart, string userId)
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