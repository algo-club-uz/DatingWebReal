using DatingWeb.Repositories.Interfaces;

namespace DatingWeb.Managers;

public class ChatManager
{
    private readonly IChatRepository _chatRepository;

    public ChatManager(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }
}