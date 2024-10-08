using ChatVivoService.DataTransferObjects.MessageDTOs;
using ChatVivoService.Hubs;
using Enitities.EntityModels;
using Enitities.Repositories.UserRepositories;
using Microsoft.AspNetCore.SignalR;

namespace ChatVivoService.Services.FileServices;

public class FileService
{
    private readonly IUserRepositoy _userRepository;
    private readonly IHubContext<ChatHub> _hubContext;
    private readonly IMessageService _messageService;

    public FileService(
        IUserRepositoy userRepository,
        IHubContext<ChatHub> hubContext,
        IMessageService messageService)
    {
        _userRepository = userRepository;
        _hubContext = hubContext;
        _messageService = messageService;
    }

    public async Task<Message> SendFilePathAsync(string filePath, MessageDTO userHelperDTO)
    {
        var message = new Message()
        {
            DocPath = filePath,
            IsRead = false,
            SentDateTime = DateTime.Now,
            CreatedAt = DateTime.Now,
            SenderId = userHelperDTO.SenderId,
            ChatId = userHelperDTO.ChatId
        };

       return await this._messageService.CreateMessageObjectAsync(message);
    }
}
