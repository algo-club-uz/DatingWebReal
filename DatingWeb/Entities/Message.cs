namespace DatingWeb.Entities;

public class Message
{
    public Guid MessageId { get; set; }

    public Guid FromUser { get; set; }

    public Guid ToUser { get; set; }

    public required string Text { get; set; }
}