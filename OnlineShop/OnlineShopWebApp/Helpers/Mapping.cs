using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Helpers;

public static class Mapping<T, K> where T : new()
{
    public static List<T> ToViewModelList(List<K> modelList)
    {
        var modelViewList = new List<T>();
        foreach (var model in modelList)
        {
            modelViewList.Add(ToViewModel(model));
        }

        return modelViewList;
    }

    public static T ToViewModel(K model)
    {
        if (model == null)
            return new T();
        var propertiesViewModel = typeof(T).GetProperties();
        var propertiesModel = typeof(K).GetProperties();
        var viewModel = new T();
        for (var i = 0; i < propertiesModel.Length && i < propertiesViewModel.Length; i++)
        {
            if (propertiesViewModel[i].Name == propertiesModel[i].Name)
            {
                if (propertiesViewModel[i].PropertyType != propertiesModel[i].PropertyType &&
                    propertiesModel[i].GetValue(model) is List<Product>)
                {
                    var propertyValue = propertiesModel[i].GetValue(model);
                    var productList = 
                        Mapping<ProductViewModel, Product>.ToViewModelList((List<Product>)propertyValue);
                    propertiesViewModel[i].SetValue(viewModel, productList);
                    continue;
                }

                if (propertiesViewModel[i].PropertyType != propertiesModel[i].PropertyType &&
                    propertiesModel[i].GetValue(model) is List<CartItem>)
                {
                    var propertyValue = propertiesModel[i].GetValue(model);
                    var cartItemList =
                        Mapping<CartItemViewModel, CartItem>.ToViewModelList((List<CartItem>)propertyValue);
                    propertiesViewModel[i].SetValue(viewModel, cartItemList);
                    continue;
                }

                if (propertiesViewModel[i].PropertyType != propertiesModel[i].PropertyType &&
                    propertiesModel[i].GetValue(model) is Product)
                {
                    var propertyValue = propertiesModel[i].GetValue(model);
                    var productViewModel = Mapping<ProductViewModel, Product>.ToViewModel((Product)propertyValue);
                    propertiesViewModel[i].SetValue(viewModel, productViewModel);
                    continue;
                }
                
                if (propertiesViewModel[i].PropertyType != propertiesModel[i].PropertyType &&
                    propertiesModel[i].GetValue(model) is Cart)
                {
                    var propertyValue = propertiesModel[i].GetValue(model);
                    var cartViewModel = Mapping<CartViewModel, Cart>.ToViewModel((Cart)propertyValue);
                    propertiesViewModel[i].SetValue(viewModel, cartViewModel);
                    continue;
                }
                
                if (propertiesViewModel[i].PropertyType != propertiesModel[i].PropertyType &&
                    propertiesModel[i].GetValue(model) is UserInfo)
                {
                    var propertyValue = propertiesModel[i].GetValue(model);
                    var userInfoViewModel = Mapping<UserInfoViewModel, UserInfo>.ToViewModel((UserInfo)propertyValue);
                    propertiesViewModel[i].SetValue(viewModel, userInfoViewModel);
                    continue;
                }

                propertiesViewModel[i].SetValue(viewModel, propertiesModel[i].GetValue(model));
            }
        }

        return viewModel;
    }
}