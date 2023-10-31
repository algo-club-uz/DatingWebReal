using DatingWeb.Context;
using DatingWeb.Entities;
using DatingWeb.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DatingWeb.Repositories;

public class ChatRepository: IChatRepository
{
    private readonly AppDbContext _context;

    public ChatRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Chat> StartOrContinueChat(Guid currentUserId, Guid toUserId)
    {
        var chat = await _context.Chats.FirstOrDefaultAsync(c =>
            c.UserIds.Contains(currentUserId) && c.UserIds.Contains(toUserId));
        if (chat is null)
        {
            chat = new Chat
            {
                UserIds = {currentUserId,toUserId},
                Messages = new List<Message>()
            };
            _context.Chats.Add(chat);
           await _context.SaveChangesAsync();
        }
        else
        {
            chat.Messages = await _context.Messages.Where(m =>
                (m.ToUser == toUserId && m.FromUser == currentUserId) ||
                (m.ToUser == currentUserId && m.FromUser == toUserId)).ToListAsync();
        }

        return chat;
    }

    public async Task<Message> SendMessage(Guid fromUserId, Guid toUserId, string message)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteMessage(Guid fromUserId, Guid toUserId, Guid messageId)
    {
        throw new NotImplementedException();
    }
}