using DatingWeb.Entities.Enums;

namespace DatingWeb.Models;

public class UserModel
{
    public Guid UserId { get; set; }
    public  string Firstname { get; set; }
    public string? Lastname { get; set; }
    public  string Username { get; set; }
    public string? Country { get; set; }
    public string? PhotoUrl { get; set; }
    public int Age { get; set; }
    public EGender Gender { get; set; }
    public DateTime CreateDate { get; set; }
}