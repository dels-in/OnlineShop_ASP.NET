using OnlineShop.Db.Models;

namespace OnlineShop.Db;

public class ProductsDbStorage : IProductStorage
{
    private readonly DatabaseContext _dbContext;

    public ProductsDbStorage(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Product> GetAll()
    {
        return _dbContext.Products.ToList();
    }

    public Product GetProduct(int productId)
    {
        return _dbContext.Products.FirstOrDefault(p => p.Id == productId);
    }

    public void Add(Product product)
    {
        _dbContext.Products.Add(product);
        _dbContext.SaveChanges();
    }

    public void Edit(Product product)
    {
        var productToChange = GetProduct(product.Id);
        if (productToChange == null) return;
        productToChange.Name = product.Name;
        productToChange.Cost = product.Cost;
        productToChange.Description = product.Description;
        productToChange.Source = product.Source;
        productToChange.MetacriticScore = product.MetacriticScore;
        productToChange.Genre = product.Genre;
        _dbContext.SaveChanges();
    }

    public void Delete(int productId)
    {
        _dbContext.Products.Remove(GetProduct(productId));
        _dbContext.SaveChanges();
    }
}