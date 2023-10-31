using System.ComponentModel.DataAnnotations;
namespace DatingWeb.Entities;

public class Chat
{
    [Key]
    public Guid ChatId { get; set; }

    public List<Guid> UserIds { get; set; }

    public List<Message> Messages { get; set; }

}