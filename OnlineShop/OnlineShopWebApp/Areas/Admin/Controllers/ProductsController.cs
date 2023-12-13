using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Helpers;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ProductsController : Controller
{
    private readonly IProductStorage _productDbStorage;
    private readonly IWebHostEnvironment appEnvironment;

    public ProductsController(IProductStorage productDbStorage, IWebHostEnvironment appEnvironment)
    {
        _productDbStorage = productDbStorage;
        this.appEnvironment = appEnvironment;
    }

    public IActionResult Index()
    {
        return View(Mapping<ProductViewModel, Product>.ToViewModelList(_productDbStorage.GetAll()));
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(ProductViewModel productViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(productViewModel);
        }

        var productImagesPath = Path.Combine(appEnvironment.WebRootPath, "/images/products/");
        if (!Directory.Exists(productImagesPath))
        {
            Directory.CreateDirectory(productImagesPath);
        }

        FileHelper.SaveImage(productViewModel.UploadedFile, productImagesPath + productViewModel.UploadedFile.FileName);
        productViewModel.Source = "/images/products/" + productViewModel.UploadedFile.FileName;

        _productDbStorage.Add(Mapping<Product, ProductViewModel>.ToViewModel(productViewModel));
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int productId)
    {
        return View(Mapping<ProductViewModel, Product>.ToViewModel(_productDbStorage.GetProduct(productId)));
    }

    [HttpPost]
    public IActionResult Edit(ProductViewModel productViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(productViewModel);
        }

        var productImagesPath = Path.Combine(appEnvironment.WebRootPath, "/images/products/");
        if (!Directory.Exists(productImagesPath))
        {
            Directory.CreateDirectory(productImagesPath);
        }

        if (productViewModel.UploadedFile != null)
        {
            FileHelper.SaveImage(productViewModel.UploadedFile,
                productImagesPath + productViewModel.UploadedFile.FileName);
            productViewModel.Source = "/images/products/" + productViewModel.UploadedFile.FileName;
        }

        _productDbStorage.Edit(Mapping<Product, ProductViewModel>.ToViewModel(productViewModel));
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int productId)
    {
        _productDbStorage.Delete(productId);
        return RedirectToAction("Index");
    }
}