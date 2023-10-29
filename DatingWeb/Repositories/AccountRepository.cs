using DatingWeb.Context;
using DatingWeb.Entities;
using DatingWeb.Entities.Enums;
using DatingWeb.Exceptions;
using DatingWeb.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DatingWeb.Repositories;

public class AccountRepository: IAccountRepository
{
    private readonly AppDbContext _identityDbContext;

    public AccountRepository(AppDbContext identityDbContext)
    {
        _identityDbContext = identityDbContext;
    }

    public async Task<List<User>> GetAllUser(Guid userId,EGender gender)
    {
       return await _identityDbContext.Users.Where(u => u.UserId != userId && u.Gender != gender).ToListAsync();
    }

    public async Task AddUser(User user)
    {
        await _identityDbContext.Users.AddAsync(user);
        await _identityDbContext.SaveChangesAsync();
    }

    public async Task<User> GetUserByUserName(string username)
    {
        var user = await _identityDbContext.Users.FirstOrDefaultAsync(i => i.Email == username);
        if (user == null)
        {
            throw new UserNotFoundException(username);
        }
        return user;
    }

    public async Task<User> GetUserById(Guid userId)
    {
        var user = await _identityDbContext.Users.FirstOrDefaultAsync(i => i.UserId == userId);
        if (user == null)
        {
            throw new UserNotFoundException(userId.ToString());
        }

        return user;
    }
    public async Task<bool> IsUserNameExist(string userName)
    {
        var isExists = await _identityDbContext.Users.
            Where(i => i.Email == userName).AnyAsync();
        return isExists;
    }

}