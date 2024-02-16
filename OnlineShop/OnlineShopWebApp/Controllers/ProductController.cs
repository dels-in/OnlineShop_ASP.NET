using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShop.ReviewApi;
using OnlineShop.ReviewApi.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers;

public class ProductController : Controller
{
    private readonly IProductStorage _productDbStorage;
    private readonly ReviewApiClient _reviewApiClient;

    public ProductController(IProductStorage productDbStorage, ReviewApiClient reviewApiClient)
    {
        _productDbStorage = productDbStorage;
        _reviewApiClient = reviewApiClient;
    }

    public IActionResult Index()
    {
        return View(Mapping<ProductViewModel, Product>.ToViewModelList(_productDbStorage.GetAll()));
    }

    public async Task<IActionResult> Details(int productId)
    {
        var product = _productDbStorage.GetProduct(productId);
        var productViewModel = Mapping<ProductViewModel, Product>.ToViewModel(product);
        
        var reviews = await _reviewApiClient.GetByProductIdAsync(productId);
        productViewModel.Reviews = Mapping<ReviewViewModel, Review>.ToViewModelList(reviews);
        
        return View(productViewModel);
    }

    public IActionResult AddReview(int productId)
    {
        var newReview = new AddReviewViewModel { ProductId = productId };
        return View();  
    }
}