using DatingWeb.Entities;

namespace DatingWeb.Repositories.Interfaces;

public interface IChatRepository
{
    Task<Chat> StartOrContinueChat(Guid currentUserId, Guid toUserId);
    Task<Message> SendMessage(Guid fromUserId, Guid toUserId, string message);
    Task DeleteMessage( Guid messageId);
    string FindUsername(Guid userId);
}