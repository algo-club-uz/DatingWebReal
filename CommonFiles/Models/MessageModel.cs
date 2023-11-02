namespace CommonFiles.Models;

public class MessageModel
{
    public Guid MessageId { get; set; }

    //from username 
    public string FromUser { get; set; }

    //to username
    public string ToUser { get; set; }

    public required string Text { get; set; }
}