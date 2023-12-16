using CommonFiles.Models;
using DatingWeb.Exceptions;
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
    private Guid currentUserId;

    public ChatsController(ChatManager chatManager, UserProvider userProvider)
    {
        _chatManager = chatManager;
        _userProvider = userProvider;
    }

    [HttpGet("GetChats")]
    public async Task<IActionResult> GetChats()
    {
        currentUserId = _userProvider.UserId;
        try
        {
            var chats = await _chatManager.GetChats(currentUserId);
            return Ok(chats);
        }
        catch (UserNotFoundException e)
        {
            return Unauthorized();
        }
    }

    [HttpGet("StartOrContinueChat")]
    public async Task<IActionResult> StartOrContinueChat(Guid toUserId)
    {
        currentUserId = _userProvider.UserId;
        try
        {
            var chat = await _chatManager.StartOrContinueChat(currentUserId, toUserId);
            return Ok(chat);
        }
        catch (UserNotFoundException e)
        {
            return Unauthorized();
        }
    }

    [HttpPost("{chatId}/SendMessage")]
    public async Task<IActionResult> SendMessage(Guid chatId, Guid toUserId, string text)
    {
        currentUserId = _userProvider.UserId;
        try
        {
            var message = await _chatManager.SendMessage(chatId: chatId, currentUserId, toUserId, text);
            return Ok(message);
        }
        catch (UserNotFoundException e)
        {
            return Unauthorized();
        }
    }

    [HttpGet("{chatId}/DeleteMessage")]
    public async Task<IActionResult> DeleteMessage(Guid chatId,Guid messageId)
    {
        return Ok(await _chatManager.DeleteMessage(chatId:chatId,messageId:messageId));
    }

    [HttpGet("GetFriends")]
    public async Task<IActionResult> GetFriends()
    {
        try
        {
            return Ok(await _chatManager.GetFriends(_userProvider.UserId));
        }
        catch (UserNotFoundException e)
        {
            return Unauthorized();
        }
    }

    [HttpGet("{requestId}/CheckRequest")]
    public async Task<IActionResult> CheckRequest(Guid requestId)
    {
        return Ok(await _chatManager.CheckRequest(requestId));
    }

    [HttpGet("SendRequest")]
    public async Task<IActionResult> SendRequest(CreateRequestModel model)
    {
        try
        {
            return Ok(await _chatManager.SendRequest(_userProvider.UserId,model));
        }
        catch (UserNotFoundException e)
        {
            return Unauthorized();
        }
    }

}