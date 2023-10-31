using DatingWeb.Managers;
using DatingWeb.Models;
using DatingWeb.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingWeb.Controllers;

[Route("api/[controller]")]
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

    [HttpGet("GetChat")]
    public async Task<IActionResult> GetChat(Guid userId)
    {
        CurrentUserId = _userProvider.UserId;
        var chat = await _chatManager.GetChat(CurrentUserId, userId);
        return Ok(chat);
    }

    [HttpGet("StartOrContinueChat")]
    public async Task<IActionResult> StartOrContinueChat(Guid toUserId)
    {
        CurrentUserId = _userProvider.UserId;
        var chat = await _chatManager.StartOrContinueChat(CurrentUserId, toUserId);
        return Ok(chat);
    }

    [HttpPost("SendMessage")]
    public async Task<IActionResult> SendMessage(Guid toUserId, string text)
    {
        CurrentUserId = _userProvider.UserId;
        var message = await _chatManager.SendMessage(CurrentUserId, toUserId, text);
        return Ok(message);
    }

    [HttpGet("DeleteMessage")]
    public async Task<IActionResult> DeleteMessage(Guid messageId)
    {
        return Ok(await _chatManager.DeleteMessage(messageId));
    }
}