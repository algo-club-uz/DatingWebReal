using DatingWeb.Entities;
using DatingWeb.Entities.Enums;
using DatingWeb.Models;

namespace DatingWeb.Repositories.Interfaces;

public interface IAccountRepository
{
    Task<List<User>> GetAllUser(Guid userId,EGender gender);
    Task AddUser(User user);
    Task<User> GetUserByUserName(string username);
    Task<User> GetUserById(Guid userId);
    Task<bool> IsUserNameExist(string userName);

}