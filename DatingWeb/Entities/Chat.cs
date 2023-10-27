using System.ComponentModel.DataAnnotations;

namespace DatingWeb.Entities;

public class Chat
{
    [Key]
    public Guid ChatId { get; set; }
}