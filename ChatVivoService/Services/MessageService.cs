using ChatVivoService.DataTransferObjects;
using ChatVivoService.Hubs;
using Enitities.EntityModels;
using Enitities.Repositories;
using Enitities.Repositories.MessageRepositories;
using Enitities.Repositories.UserRepositories;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace ChatVivoService.Services;

public class MessageService : IMessageService
{
    private readonly IMessageRepository _messageRepository;
    private readonly IUserRepositoy _userRepository;
    private readonly IHubContext<ChatHub> _chatHubContext;

    public MessageService(
        IMessageRepository messageRepository,
        IHubContext<ChatHub> chatHubContext,
        IUserRepositoy userRepository)
    {
        _messageRepository = messageRepository;
        _chatHubContext = chatHubContext;
        this._userRepository = userRepository;
    }

    public async Task<Message> CreateMessageAsync(CreateMessageDTO createMessageDTO)
    {
        var storedMessage = await this._messageRepository.SelectByIdAsync(createMessageDTO.Id);

        if (storedMessage != null)
        {
            throw new Exception($"Message already exist with such id {createMessageDTO.Id}");
        }

        Message message = new Message
        {
            SenderId = createMessageDTO.SenderId,
            ChatId = createMessageDTO.ChatId,
            IsRead = false,
            SentDateTime = DateTime.Now,
            CreatedAt = DateTime.Now,
            Text = createMessageDTO.Message,
         
        };
        var inserted =  await this._messageRepository.InsertAsync(message);

        var userModerator = await this._userRepository.SelectByExpressionAsync(user => user.Id == createMessageDTO.SenderId && user.IsModerator, new string[] { }).FirstOrDefaultAsync();

        if(userModerator != null)
        {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           
        }

        return message;
    }

    public async Task DeleteMessageAsync(Message message)
    {
        var storedMessage = await this._messageRepository.SelectByIdAsync(message.Id);

        if (storedMessage == null)
        {
            throw new Exception($"Message not exist with such id {message.Id}");
        }

        await this._messageRepository.DeleteAsync(message);

    }

    public IQueryable<Message> GetAllMessagesByUserId(int userId)
    {
        var messages = this._messageRepository.SelectByExpressionAsync(message => message.SenderId == userId, new string[] { });

        return messages;
    }
    
    public async Task<Message> UpdateMessageAsync(ModifyMessageDTO modifyMessageDTO)
    {
        var storedMessage = await this._messageRepository.SelectByIdAsync(modifyMessageDTO.Id);

        if (storedMessage == null)
        {
            throw new Exception($"Message not exist with such id {modifyMessageDTO.Id}");
        }

        storedMessage.Text = modifyMessageDTO.Message ?? storedMessage.Text;
        storedMessage.IsRead = modifyMessageDTO.IsRead ?? storedMessage.IsRead;

        var updateMessage = await this._messageRepository.UpdateAsync(storedMessage);

        return updateMessage;
    }

    public async Task<Message> UpdateMessageStatusAsync(int messageId)
    {
        //var storedMessage = await this._messageRepository.SelectByIdAsync(messageId);
        var storedMessage = await this._messageRepository.SelectByExpressionAsync(message => message.Id == messageId, new string[] {"Chat" }).FirstAsync();

        if (storedMessage == null)
            throw new Exception($"Message not found with id such {messageId}");

        storedMessage.IsRead = true;

        var updatedMessage = await this._messageRepository.UpdateAsync(storedMessage);

        var clientUserId = storedMessage.ChatId;

        var currentUserClient = await this._userRepository.SelectByIdAsync(clientUserId);

        await this._chatHubContext.Clients.Client(currentUserClient.ConnectionId).SendAsync("OnMessageStatusChanged", storedMessage);
        await this._chatHubContext.Clients.Group("Admins").SendAsync("OnMessageStatusChanged", storedMessage);

        return storedMessage;
    }
}
