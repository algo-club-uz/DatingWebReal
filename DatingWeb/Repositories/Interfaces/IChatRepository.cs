namespace DatingWeb.Repositories.Interfaces;

public interface IChatRepository
{
    Task StartOrContinueChat(Guid currentUserId, Guid toUserId);
    Task SendMessage(Guid fromUserId, Guid toUserId, string message);
    Task DeleteMessage(Guid fromUserId, Guid toUserId, Guid messageId);
}