using CommonFiles.Enums;
using DatingWeb.Entities;

namespace DatingWeb.Repositories.Interfaces;

public interface IAccountRepository
{
    Task<List<User>> GetAllUser(Guid userId,EGender gender);
    Task AddUser(User user);
    Task<User> GetUserByUserName(string username);
    Task<User> GetUserById(Guid userId);
    Task<bool> IsUserNameExist(string userName);

    Task EditAccount(User user);

}