using ChatVivoService.Services.ChatServices;
using Enitities.EntityModels;
using Microsoft.AspNetCore.Mvc;

namespace ChatVivo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;

    public ChatController(IChatService chatService)
    {
        this._chatService = chatService;
    }

    [HttpPost]
    public async Task<Chat> CreateChatAsync(string chatName, int userId)
    {
        var createdChat = await this._chatService
            .CreateChatAsync(chatName, userId);

        return createdChat;
    }

    [HttpGet("userId")]
    public IQueryable<Chat> GetAllChatsByUserId(int userId)
    {
        var chatsByUserId = this._chatService
            .GetChatsByUserId(userId);

        return chatsByUserId;
    }

    [HttpPut("chatId")]
    public async Task<Chat> ModifyChatStatusAsync(int chatId)
    {
        var updatedChat = await this._chatService
            .UpdateChatStatusAsync(chatId);

        return updatedChat;
    }
}
