using CommonFiles.Enums;
using CommonFiles.Models;
using DatingWeb.Entities;
using DatingWeb.Exceptions;
using DatingWeb.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DatingWeb.Managers;

public class UserManager
{
    private readonly IAccountRepository _userRepository;
    private readonly JwtTokenManager _jwtTokenManager;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public UserManager(JwtTokenManager jwtTokenManager, IAccountRepository userRepository, IWebHostEnvironment webHostEnvironment)
    {
        _jwtTokenManager = jwtTokenManager;
        _userRepository = userRepository;
        _webHostEnvironment = webHostEnvironment;
    }


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
            Age = model.Age,
            Country = model.Country,
            Gender = ParseToEnum(model.Gender),
            //UserRole = ERole.User
        };
        user.PhotoUrl = SaveAvatar(user.Gender);

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

    public async Task<List<UserModel>> GetAllUser(Guid userId, EGender gender)
    {
        var users = await _userRepository.GetAllUser(userId, gender);
        var userModels = new List<UserModel>();
        foreach (var user in users)
        {
            userModels.Add(ParseToUserModel(user));
        }
        return userModels;
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

    public async Task<UserModel> EditAccount(Guid userId, EditUserModel model, IFormFile? image)
    {
        var user = await _userRepository.GetUserById(userId);
        string wwRootPath = _webHostEnvironment.WebRootPath;
        if (image != null)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            string productPath = Path.Combine(wwRootPath, @"images\user");

            if (!string.IsNullOrEmpty(user.PhotoUrl))
            {
                if (user.PhotoUrl != @"\images\user\male_avatar.jpg" &&
                    user.PhotoUrl != @"\images\user\male_avatar.jpg")
                {
                    //delete the old image
                    var oldImagePath = Path
                        .Combine(wwRootPath, user.PhotoUrl.TrimStart('\\'));

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
            }

            await using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }

            user.PhotoUrl = @"\images\user\" + fileName;
        }

        if (!string.IsNullOrEmpty(model.FirstName))
        {
            user.FirstName = model.FirstName;
        }
        if (!string.IsNullOrEmpty(model.LastName))
        {
            user.LastName = model.LastName;
        }
        if (!string.IsNullOrEmpty(model.Country))
        {
            user.Country = model.Country;
        }
        if (!string.IsNullOrEmpty(model.Gender))
        {
            user.Gender = ParseToEnum(model.Gender);
        }

        if (model.Age is not null and not 0)
        {
            user.Age = (int)model.Age;
        }

        await _userRepository.EditAccount(user);

        return ParseToUserModel(user);

    }

    private UserModel ParseToUserModel(User user)
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

    private EGender ParseToEnum(string text)
    {
        var gender = text == "Male" ? EGender.Male : EGender.Female;
        return gender;
    }

    private string SaveAvatar(EGender gender)
    {
        if (gender is EGender.Male)
        {
            return @"\images\user\male_avatar.jpg";
        }
        else
        {
            return @"\images\user\female_avatar.jpg";
        }
    }
}