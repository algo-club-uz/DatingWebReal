using DatingWeb.Entities.Enums;

namespace DatingWeb.Entities;

public class User
{
    public Guid UserId { get; set; }

    public required string FirstName { get; set; }
    public string? LastName { get; set; }

    public required string Email { get; set; }
    public bool? EmailConfirmed { get; set; } = false;

    public required string Password { get; set; }
    public required string ConfirmationPassword { get; set; }

    public string? Country { get; set; }
    public string? PhotoUrl { get; set; }
    public required int Age { get; set; }
    public  EGender Gender { get; set; }


}