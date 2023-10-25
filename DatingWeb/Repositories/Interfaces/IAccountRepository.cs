using DatingWeb.Entities;
using DatingWeb.Models;

namespace DatingWeb.Repositories.Interfaces;

public interface IAccountRepository
{
    Task<UserModel> Register(CreateUserModel model);

    Task<UserModel?> GetUser(Guid id);

    Task<string> Login(LoginUserModel model);
    Task<UserModel?> GetUser(string username);

}