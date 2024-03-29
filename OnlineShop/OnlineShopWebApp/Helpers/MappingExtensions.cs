using Microsoft.AspNetCore.Identity;
using OnlineShop.Db.Helpers;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Helpers;

public static class MappingExtensions
{
    public static List<UserViewModel> ToUserViewModelList(this List<User> userList)
    {
        var userViewModelList = new List<UserViewModel>();
        foreach (var user in userList)
        {
            userViewModelList.Add(ToUserViewModel(user));
        }

        return userViewModelList;
    }

    public static UserViewModel ToUserViewModel(this User user)
    {
        return new UserViewModel
        {
            Id = user.Id,
            Email = user.Email,
            Password = user.Password,
            ConfirmPassword = user.ConfirmPassword,
            Picture = user.Picture,
            Roles = user.Roles,
        };
    }

    public static User ToUser(this UserViewModel userViewModel)
    {
        return new User
        {
            Id = userViewModel.Id,
            Email = userViewModel.Email,
            UserName = userViewModel.Email,
            Password = userViewModel.Password.Encrypt(),
            ConfirmPassword = userViewModel.ConfirmPassword.Encrypt(),
            Picture = userViewModel.Picture,
            Roles = userViewModel.Roles,
        };
    }

    public static List<RoleViewModel> ToRoleViewModelList(this List<Role> rolesList)
    {
        var roleViewModelList = new List<RoleViewModel>();
        foreach (var role in rolesList)
        {
            roleViewModelList.Add(ToRoleViewModel(role));
        }

        return roleViewModelList;
    }

    public static RoleViewModel ToRoleViewModel(this Role role)
    {
        return new RoleViewModel
        {
            Name = role.Name,
        };
    }
    
    public static Role ToRole(this RoleViewModel roleViewModel)
    {
        return new Role
        {
            Name = roleViewModel.Name,
        };
    }
}