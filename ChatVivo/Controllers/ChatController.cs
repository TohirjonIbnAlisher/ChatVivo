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

    [HttpDelete("id")]
    public async Task<Chat> DeleteChatAsync(int chatId)
    {
        var deletedChat = await this._chatService
            .DeleteChatAsync(chatId);

        return deletedChat;
    }

    [HttpGet("userId")]
    public IQueryable<Chat> GetAllChatsByUserId(int userId)
    {
        var chatsByUserId = this._chatService
            .GetChatsByUserId(userId);

        return chatsByUserId;
    }
}
