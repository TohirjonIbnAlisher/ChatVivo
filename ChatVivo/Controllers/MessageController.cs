using ChatVivoService.DataTransferObjects.MessageDTOs;
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
        [FromBody] CreationMessageTextDTO dto)
    {
        var cretedMessage = await this._messageService.CreateMessageAsync(dto);
        return Ok(cretedMessage);
    }

    [HttpGet("GetMessagesByUserId")]
    public async Task<IQueryable<Message>> GetAllMessagesByUserId(ParameterMessageDTO dto)
    {
        var messages = await this._messageService.GetAllMessagesByUserId(dto);

        return messages;
    }

}