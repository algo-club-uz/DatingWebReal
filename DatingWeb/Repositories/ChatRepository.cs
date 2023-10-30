using DatingWeb.Repositories.Interfaces;

namespace DatingWeb.Repositories;

public class ChatRepository: IChatRepository
{
    public async Task StartOrContinueChat(Guid currentUserId, Guid toUserId)
    {
        throw new NotImplementedException();
    }

    public async Task SendMessage(Guid fromUserId, Guid toUserId, string message)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteMessage(Guid fromUserId, Guid toUserId, Guid messageId)
    {
        throw new NotImplementedException();
    }
}