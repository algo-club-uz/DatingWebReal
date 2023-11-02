namespace CommonFiles.Models;

public class ChatModel
{
    public Guid ChatId { get; set; }

    public List<string> Usernames { get; set; }

    public List<MessageModel> Messages { get; set; }

}