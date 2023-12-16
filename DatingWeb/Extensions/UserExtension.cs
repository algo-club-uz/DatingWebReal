using CommonFiles.Models;
using DatingWeb.Entities;

namespace DatingWeb.Extensions;

public static class UserExtension
{
    public static UserModel ParseToUserModel(this User user)
    {
        var model = new UserModel()
        {
            UserId = user.UserId,
            Firstname = user.FirstName,
            Lastname = user.LastName,
            CreateDate = user.CreateDate,
            Username = user.Email,
            Age = user.Age,
            Gender = user.Gender,
            Country = user.Country,
            PhotoUrl = user.PhotoUrl,
            //UserRole = user.UserRole
        };
        return model;
    }

    public static List<UserModel> ParseToListUserModel(List<User>? users)
    {
        if (users is null)
        {
            return new List<UserModel>();
        }
        var userModels = new List<UserModel>();
        foreach (var model in users)
        {
            userModels.Add(model.ParseToUserModel());
        }
        return userModels;
    }
}