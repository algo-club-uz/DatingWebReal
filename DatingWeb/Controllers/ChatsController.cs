using DatingWeb.Managers;
using DatingWeb.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingWeb.Controllers;

[Route("api/[controller]/userId/")]
[ApiController]
[Authorize]
public class ChatsController : ControllerBase
{
    private readonly ChatManager _chatManager; 
    private readonly UserProvider _userProvider;
    private Guid CurrentUserId;

    public ChatsController(ChatManager chatManager, UserProvider userProvider)
    {
        _chatManager = chatManager;
        _userProvider = userProvider;
    }

    [HttpGet("GetChats")]
    public async Task<IActionResult> GetChats()
    {
        CurrentUserId = _userProvider.UserId;
        var chats = await _chatManager.GetChats(CurrentUserId);
        return Ok(chats);
    }

    [HttpGet("StartOrContinueChat")]
    public async Task<IActionResult> StartOrContinueChat(Guid toUserId)
    {
        CurrentUserId = _userProvider.UserId;
        var chat = await _chatManager.StartOrContinueChat(CurrentUserId, toUserId);
        return Ok(chat);
    }

    [HttpPost("{chatId}/SendMessage")]
    public async Task<IActionResult> SendMessage(Guid chatId, Guid toUserId, string text)
    {
        CurrentUserId = _userProvider.UserId;
        var message = await _chatManager.SendMessage(chatId:chatId,CurrentUserId, toUserId, text);
        return Ok(message);
    }

    [HttpGet("{chatId}/DeleteMessage")]
    public async Task<IActionResult> DeleteMessage(Guid chatId,Guid messageId)
    {
        return Ok(await _chatManager.DeleteMessage(chatId:chatId,messageId:messageId));
    }
}