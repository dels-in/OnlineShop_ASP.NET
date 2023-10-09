using Microsoft.AspNetCore.Mvc;
using ReturnTrue.AspNetCore.Identity.Anonymous;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class CheckoutController : Controller
{
    private readonly ICartsStorage _inMemoryCartsStorage;
    private readonly IPersonalDataStorage _inMemoryPersonalDataStorage;


    public CheckoutController(ICartsStorage inMemoryCartsStorage, IPersonalDataStorage inMemoryPersonalDataStorage)
    {
        _inMemoryCartsStorage = inMemoryCartsStorage;
        _inMemoryPersonalDataStorage = inMemoryPersonalDataStorage;
    }

    public IActionResult Index()
    {
        return View();
    }

    public RedirectToActionResult Details(ValidationModel.PersonalData personalData)
    {
        _inMemoryPersonalDataStorage.AddToList(personalData);
        return RedirectToAction("Checkout");
    }

    public IActionResult Checkout()
    {
        return View(_inMemoryCartsStorage.GetByUserId(GetUserId()));
    }

    private string GetUserId()
    {
        var feature = HttpContext.Features.Get<IAnonymousIdFeature>();
        return feature.AnonymousId ?? "007";
    }
}