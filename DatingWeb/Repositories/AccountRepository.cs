using DatingWeb.Entities;
using DatingWeb.Exceptions;
using DatingWeb.Models;
using DatingWeb.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DatingWeb.Repositories;

public class AccountRepository: IAccountRepository
{
    //private readonly IUserRepository _userRepository;
    //private readonly JwtTokenManager _jwtTokenManager;

    /*public UserManager(JwtTokenManager jwtTokenManager, IUserRepository userRepository)
    {
        _jwtTokenManager = jwtTokenManager;
        _userRepository = userRepository;
    }*/


    public async Task<UserModel> Register(CreateUserModel model)
    {
        if (await _userRepository.IsUserNameExist(model.Username))
        {
            throw new UsernameExistException(model.Username);
        }
        var user = new User()
        {
            FirstName = model.Firstname,
            LastName = model.Lastname,
            Email = model.Username,
            //UserRole = ERole.User
        };

        user.PasswordHash = new PasswordHasher<User>().HashPassword(user, model.Password);
        await _userRepository.AddUser(user);
        return ParseToUserModel(user);
    }

    public async Task<string> Login(LoginUserModel model)
    {
        var userName = await _userRepository.GetUserByUserName(model.Username);
        var result = new PasswordHasher<User>().
            VerifyHashedPassword(userName, userName.PasswordHash, model.Password);

        if (result == PasswordVerificationResult.Failed)
        {
            throw new InCorrectPassword(model.Password);
        }
        var token = _jwtTokenManager.GenerateToken(userName);
        return token;
    }

    public async Task<UserModel?> GetUser(string username)
    {
        var user = await _userRepository.GetUserByUserName(username);
        return ParseToUserModel(user);
    }
    public async Task<UserModel?> GetUser(Guid id)
    {
        var user = await _userRepository.GetUserById(id);
        return ParseToUserModel(user);
    }

    private UserModel ParseToUserModel(User user)
    {
        var model = new UserModel()
        {
            UserId = user.UserId,
            Firstname = user.FirstName,
            Lastname = user.LastName,
            //CreateDate = user.CreateDate,
            Username = user.Email,
            //UserRole = user.UserRole
        };
        return model;
    }
}