using DatingWeb.Context;
using DatingWeb.Entities;
using DatingWeb.Exceptions;
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

    public async Task<List<Chat>> GetChats(Guid currentUserId)
    {
        var chats = await _context.Chats.
            Where(c => c.UserIds.Contains(currentUserId))
            .ToListAsync();
        return chats;
    }

    /*public async Task<Chat> GetChat(Guid currentUserId, Guid userId)
    {
        var chat = await _context.Chats.FirstOrDefaultAsync(c =>
            c.UserIds.Contains(currentUserId) && c.UserIds.Contains(userId));
        return chat;
    }*/

    public async Task<Chat> StartOrContinueChat(Guid currentUserId, Guid toUserId)
    {
        var toUser = await _context.Users.FirstOrDefaultAsync(u => u.UserId == toUserId);
        if (toUser is not null)
        {
            var chat = await _context.Chats.FirstOrDefaultAsync(c =>
                c.UserIds.Contains(currentUserId) && c.UserIds.Contains(toUserId));
            if (chat is null)
            {
                chat = new Chat
                {
                    UserIds = new List<Guid> { currentUserId, toUserId },
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

        throw new UserNotFoundException(toUserId.ToString());
    }

    public async Task<Message> SendMessage(Guid chatId,Guid fromUserId, Guid toUserId, string text)
    {
        var chat = await _context.Chats.FirstOrDefaultAsync(c => c.ChatId == chatId);
        if (chat is not null)
        {
            var message = new Message
            {
                ToUser = toUserId,
                FromUser = fromUserId,
                Text = text,
                ChatId = chatId
            };
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            return message;
        }

        throw new ChatNotFoundException(chatId.ToString());
    }

    public async Task DeleteMessage(Guid chatId,Guid messageId)
    {
        var chat = await _context.Chats.FirstOrDefaultAsync(c => c.ChatId == chatId);
        if (chat is not null)
        {
            var message = await _context.Messages.FindAsync(messageId);
            if (message is not null)
            {
                _context.Messages.Remove(message);
                await _context.SaveChangesAsync();
            }

            throw new Exception($"Message not found with {messageId}");
        }

        throw new ChatNotFoundException(chatId.ToString());
    }

    public string FindUsername(Guid userId)
    {
        var user =  _context.Users.FirstOrDefault(u => u.UserId == userId);
        return user.Email;
    }

    public async Task<List<Request>> GetRequests(Guid currentUserId)
    {
        throw new NotImplementedException();
    }

    public async Task SendRequest(Request request)
    {
        throw new NotImplementedException();
    }

    public async Task<Request> CheckRequest(Guid requestId)
    {
        throw new NotImplementedException();
    }
}