using DatingWeb.Entities;

namespace DatingWeb.Repositories.Interfaces;

public interface IChatRepository
{
    Task<List<Chat>> GetChats(Guid currentUserId);
    /*Task<Chat> GetChat(Guid currentUserId,Guid userId);*/
    Task<Chat> StartOrContinueChat(Guid currentUserId, Guid toUserId);
    Task<Message> SendMessage(Guid chatId,Guid fromUserId, Guid toUserId, string message);
    Task DeleteMessage(Guid chatId,Guid messageId);
    string FindUsername(Guid userId);

    Task<List<Request>> GetRequests(Guid currentUserId);
    Task SendRequest(Request  request);

    Task<Request> CheckRequest(Guid requestId);

}