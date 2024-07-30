using ChatVivoService.Hubs;
using Enitities.EntityModels;
using Enitities.FileModels;
using Enitities.Repositories.MessageRepositories;
using Enitities.Repositories.UserRepositories;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace ChatVivoService.Services.FileServices;

public class FileService
{
    private readonly IUserRepositoy _userRepository;
    private readonly IHubContext<ChatHub> _hubContext;
    private readonly IMessageRepository _messageRepository;

    public FileService(
        IUserRepositoy userRepository,
        IHubContext<ChatHub> hubContext,
        IMessageRepository messageRepository)
    {
        _userRepository = userRepository;
        _hubContext = hubContext;
        _messageRepository = messageRepository;
    }

    public async Task SendFilePathAsync(string filePath, UserFileHelper userHelperDTO)
    {
        if(userHelperDTO.IsModerator)
        {
            var fileSendUser = await this._userRepository.SelectByExpressionAsync(user => user.Id == userHelperDTO.ReceiverId, new string[] { }).FirstOrDefaultAsync();

            await this._hubContext.Clients.Client(fileSendUser.ConnectionId).SendAsync("OnSendDocument", filePath);
        }

        else
        {
            await this._hubContext.Clients.Group("Admin").SendAsync("OnSendDocument", filePath);
        }

        var message = new Message()
        {
            DocPath = filePath,
            IsRead = false,
            SentDateTime = DateTime.Now,
            CreatedAt = DateTime.Now,
            SenderId = userHelperDTO.SenderId,
            ChatId = userHelperDTO.ChatId
        };

        var createdMessage = await this._messageRepository.InsertAsync(message);
    }
}
