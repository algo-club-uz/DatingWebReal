using System.ComponentModel.DataAnnotations;

namespace CommonFiles.Models;

public class CreateUserModel
{
    [Required]
    public  string Firstname { get; set; }
    [Required]
    public  string Lastname { get; set; }
    [Required]
    public  string Username { get; set; }
    [Required]
    public  string Password { get; set; }
    [Compare(nameof(Password))]
    [Required]
    public  string ConfirmPassword { get; set; }
    public string? Country { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }
}