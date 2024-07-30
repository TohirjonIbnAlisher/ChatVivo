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
        await this._messageService.CreateMessageAsync(dto);
        return Ok(dto);
    }

    [HttpGet("GetMessagesByUserId")]
    public IQueryable<Message> GetAllMessagesByUserId(int userId)
    {
        var messages = this._messageService.GetAllMessagesByUserId(userId);

        return messages;
    }

    [HttpPut("id")]
    public async Task<Message> UpdateMessageStatusAsync(int messageId)
    {

    }
}