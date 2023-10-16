using System.Text.Json;
using WebApplication1.Controllers;
using WebApplication1.Models;

namespace WebApplication1;

public class InMemoryCheckoutStorage : IStorage<Validation, Checkout>
{
    private readonly IFileStorage _inMemoryFileStorage;

    private readonly List<Validation> _validations;

    public InMemoryCheckoutStorage(IFileStorage inMemoryFileStorage)
    {
        _inMemoryFileStorage = inMemoryFileStorage;
        _validations = _inMemoryFileStorage.Load<Validation>("Checkouts.json");
    }

    public void AddToList(Checkout checkout, string userId)
    {
        _validations.Add(new Validation
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Checkouts = new() { checkout }
        });
        _inMemoryFileStorage.Save(_validations, "Checkouts.json");
    }

    public void Delete(Checkout parameter, string userId)
    {
        throw new NotImplementedException();
    }

    public void Reduce(Checkout parameter, string userId)
    {
        throw new NotImplementedException();
    }

    public Validation GetByUserId(string userId)
    {
        throw new NotImplementedException();
    }
}