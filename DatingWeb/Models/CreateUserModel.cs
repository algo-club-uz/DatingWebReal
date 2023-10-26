using DatingWeb.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace DatingWeb.Models;

public class CreateUserModel
{
    public required string Firstname { get; set; }
    public required string Lastname { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    [Compare(nameof(Password))]
    public required string ConfirmPassword { get; set; }
    public string? Country { get; set; }
    public int Age { get; set; }
    public EGender Gender { get; set; }
}