using DatingWeb.Entities;
using DatingWeb.Models;

namespace DatingWeb.Repositories.Interfaces;

public interface IAccountRepository
{
    Task AddUser(User user);
    Task<User> GetUserByUserName(string username);
    Task<User> GetUserById(Guid userId);
    Task<bool> IsUserNameExist(string userName);

}