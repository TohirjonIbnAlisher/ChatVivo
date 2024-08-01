using ChatVivoService.DataTransferObjects;
using ChatVivoService.Services;
using Enitities.EntityModels;
using Microsoft.AspNetCore.Mvc;

namespace ChatVivo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessageController : ControllerBase
{
    private IMessageService _messageService;

    public MessageController(IMessageService _messageService)
    {
        this._messageService = _messageService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateMessageAsync(
        [FromBody] CreateMessageDTO dto
        )
    {
        var cretedMessage = await this._messageService.CreateMessageAsync(dto);
        return Ok(cretedMessage);
    }

    [HttpGet("GetMessagesByUserId")]
    public IQueryable<Message> GetAllMessagesByUserId(int userId)
    {
        var messages = this._messageService.GetAllMessagesByUserId(userId);

        return messages;
    }

    [HttpGet("GetMessageByChatId")]
    public IQueryable<Message> GetAllMessageByChatId(int chatId)
    {
        return this._messageService.GetAllMessagesByChatId(chatId);
    }

    [HttpPut("id")]
    public async Task<Message> UpdateMessageStatusAsync(int messageId)
    {
        var updatedMessage = await this._messageService.UpdateMessageStatusAsync(messageId);

        return updatedMessage;
    }
}