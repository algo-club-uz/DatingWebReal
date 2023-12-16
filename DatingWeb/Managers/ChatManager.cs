using CommonFiles.Enums;
using CommonFiles.Models;
using DatingWeb.Entities;
using DatingWeb.Extensions;
using DatingWeb.Repositories.Interfaces;

namespace DatingWeb.Managers;

public class ChatManager
{
    private readonly IChatRepository _chatRepository;

    public ChatManager(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }

    public async Task<List<ChatModel>> GetChats(Guid currentUserId)
    {
        var chats = await _chatRepository.GetChats(currentUserId);
        var chatModels = ToModels(chats);
        return chatModels;
    }

    /*public async Task<ChatModel> GetChat(Guid currentUserId, Guid userId)
    {
        var chat = await _chatRepository.GetChat(currentUserId, userId);
        var chatModel = ToModel(chat);
        return chatModel;
    }*/


    public async Task<ChatModel?> StartOrContinueChat(Guid currentUserId, Guid toUserId)
    {
        var chat = await _chatRepository.StartOrContinueChat(currentUserId, toUserId);
        var chatModel = ToModel(chat);
        return chatModel;
    }

    public async Task<MessageModel> SendMessage(Guid chatId,Guid fromUserId, Guid toUserId, string text)
    {
        var message = await _chatRepository.SendMessage(chatId:chatId,fromUserId: fromUserId, toUserId: toUserId, text);
        var messageModel = ToModel(message);
        return messageModel;
    }

    public async Task<string> DeleteMessage(Guid chatId,Guid messageId)
    {
        await _chatRepository.DeleteMessage(chatId:chatId,messageId:messageId);
        return "Deleted";
    }

    public async Task<List<RequestModel>?> GetRequests(Guid currentUserId)
    {
        var requests = await _chatRepository.GetRequests(currentUserId: currentUserId);
        return ToModels(requests);
    }

    public async Task<RequestModel?> CheckRequest(Guid requestId)
    {
        var request = await _chatRepository.CheckRequest(requestId:requestId);
        return ToModel(request);
    }

    public async Task<RequestModel> SendRequest(Guid currentUserId,CreateRequestModel model)
    {
        var request = new Request()
        {
            FromUser = currentUserId,
            ToUser = model.ToUser,
            Text = CheckRequestText(currentUserId,model.Text),
            Status = ERequest.Pending
        };

        await _chatRepository.SendRequest(request);
        return ToModel(request)!;
    }

    public async Task<List<UserModel>> GetFriends(Guid currentUserId)
    {
        var users = await _chatRepository.GetFriends(currentUserId);

        return UserExtension.ParseToListUserModel(users);
    }

    private ChatModel? ToModel(Chat chat)
    {
        if (chat is not null )
        {
            var usernames = new List<string>();
            foreach (var userId in chat.UserIds)
            {
                usernames.Add(_chatRepository.FindUsername(userId));
            }
            var model = new ChatModel
            {
                ChatId = chat.ChatId,
                Usernames = usernames,
                Messages = ToModels(chat.Messages)
            };
            return model;
        }
        return null;
    }

    private List<ChatModel?> ToModels(List<Chat> chats)
    {
        if (chats is not null)
        {
            var models = new List<ChatModel>();
            foreach (var chat in chats)
            {
                models.Add(ToModel(chat));
            }
            return models;
        }

        return null;
    }

    private MessageModel ToModel(Message message)
    {
        var model = new MessageModel
        {
            MessageId = message.MessageId,
            FromUser = _chatRepository.FindUsername(message.FromUser),
            ToUser = _chatRepository.FindUsername(message.ToUser),
            Text = message.Text
        };
        return model;
    }

    private List<MessageModel>? ToModels(List<Message> messages)
    {
        if (messages is not null)
        {
            var models = new List<MessageModel>();
            foreach (var message in messages)
            {
                models.Add(ToModel(message));
            }
            return models;
        }
        return null;
    }

    private RequestModel? ToModel(Request? request)
    {
        if (request is not null)
        {
            var model = new RequestModel()
            {
                FromUser = _chatRepository.FindUsername(request.FromUser),
                ToUser = _chatRepository.FindUsername(request.ToUser),
                Text = request.Text,
                Status = request.Status.ToString(),
            };
            return model;
        }

        return null;
    }

    private List<RequestModel>? ToModels(List<Request>? requests)
    {
        if (requests is not null)
        {
            var models = new List<RequestModel>();
            foreach (var request in requests)
            {
                models.Add(ToModel(request)!);
            }
            return models;
        }

        return null;
    }

    private string CheckRequestText(Guid currentUserId,string text)
    {
        if (!string.IsNullOrEmpty(text))
        {
            return text;
        }

        var username = _chatRepository.FindUsername(currentUserId);
        return $"Hi, I'm {username}. I want to chat with you. Please add me :)";
    }

}