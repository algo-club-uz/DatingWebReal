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
    private  Guid CurrentUserId => _userProvider.UserId;

    public ChatsController(ChatManager chatManager, UserProvider userProvider)
    {
        _chatManager = chatManager;
        _userProvider = userProvider;
    }

    [HttpGet("GetChats")]
    public async Task<IActionResult> GetChats(Guid currentUserId)
    {
        var chats = await _chatManager.GetChats(currentUserId);
        return Ok(chats);
    }

    [HttpGet("GetChat")]
    public async Task<IActionResult> GetChat(Guid userId)
    {
        var chat = await _chatManager.GetChat(CurrentUserId, userId);
        return Ok(chat);
    }

    [HttpGet("StartOrContinueChat")]
    public async Task<IActionResult> StartOrContinueChat(Guid toUserId)
    {
        var chat = await _chatManager.StartOrContinueChat(CurrentUserId, toUserId);
        return Ok(chat);
    }

    [HttpPost("SendMessage")]
    public async Task<IActionResult> SendMessage(Guid toUserId, string text)
    {
        var message = await _chatManager.SendMessage(CurrentUserId, toUserId, text);
        return Ok(message);
    }

    [HttpGet("DeleteMessage")]
    public async Task<IActionResult> DeleteMessage(Guid messageId)
    {
        return Ok(await _chatManager.DeleteMessage(messageId));
    }
}