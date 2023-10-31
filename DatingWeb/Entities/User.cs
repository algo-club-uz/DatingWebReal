using System.ComponentModel.DataAnnotations;
using DatingWeb.Entities.Enums;

namespace DatingWeb.Entities;

public class User
{
    [Key]
    public Guid UserId { get; set; }

    public required string FirstName { get; set; }
    public string? LastName { get; set; }

    public required string Email { get; set; }
    public bool? EmailConfirmed { get; set; } = false;
    public string PasswordHash { get; set; }

    public string? Country { get; set; }
    public string? PhotoUrl { get; set; }
    public  int Age { get; set; }
    public  EGender Gender { get; set; }
    public DateTime CreateDate { get; set; } = DateTime.UtcNow;

    public List<Chat> Chats { get; set; }
}