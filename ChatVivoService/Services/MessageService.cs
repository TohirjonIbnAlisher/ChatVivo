using ChatVivoService.DataTransferObjects;
using ChatVivoService.Hubs;
using Enitities.EntityModels;
using Enitities.Repositories.ChatRepositories;
using Enitities.Repositories.MessageRepositories;
using Enitities.Repositories.UserRepositories;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace ChatVivoService.Services;

public class MessageService : IMessageService
{
    private readonly IMessageRepository _messageRepository;
    private readonly IChatRepository _chatRepository;
    private readonly IUserRepositoy _userRepository;
    private readonly IHubContext<ChatHub> _chatHubContext;

    public MessageService(
        IMessageRepository messageRepository,
        IHubContext<ChatHub> chatHubContext,
        IUserRepositoy userRepository,
        IChatRepository chatRepository)
    {
        _messageRepository = messageRepository;
        _chatHubContext = chatHubContext;
        this._userRepository = userRepository;
        this._chatRepository = chatRepository;
    }

    public async Task<Message> CreateMessageAsync(CreateMessageDTO createMessageDTO)
    {
        Message message = new Message
        {
            SenderId = createMessageDTO.SenderId,
            ChatId = createMessageDTO.ChatId,
            IsRead = false,
            ParentId = createMessageDTO.ParentId,
            SentDateTime = DateTime.Now,
            CreatedAt = DateTime.Now,
            Text = createMessageDTO.Message,
         
        };
        var inserted =  await this._messageRepository.InsertAsync(message);

        var sentMessageWithAllRelations = await this._messageRepository.SelectByExpressionAsync(message => message.Id == inserted.Id, new string[] { "Chat", "ParentMessage", "Sender" }).AsNoTracking().FirstOrDefaultAsync();
        //var userModerator = await this._userRepository.SelectByExpressionAsync(user => user.Id == createMessageDTO.SenderId, new string[] { }).FirstOrDefaultAsync();

        if(sentMessageWithAllRelations.Sender.IsModerator)
        {
            var chatData = await this._chatRepository.SelectByExpressionAsync(chat => chat.Id == createMessageDTO.ChatId, new string[] { "User" }).AsNoTracking().FirstOrDefaultAsync();

            await this._chatHubContext.Clients.Client(chatData.User.ConnectionId).SendAsync("OnSendMessage", sentMessageWithAllRelations);
        }
        else
        {
            await this._chatHubContext.Clients.Group("Admin").SendAsync("OnSendMessage", sentMessageWithAllRelations);
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
        var messages = this._messageRepository.SelectByExpressionAsync(message => message.SenderId == userId, new string[] { "Chat", "ParentMessage", "Sender" });

        return messages;
    }

    public IQueryable<Message> GetAllMessagesByChatId(int chatId)
    {
        var messages = this._messageRepository.SelectByExpressionAsync(message => message.ChatId == chatId, new string[] { "Chat", "ParentMessage", "Sender" });

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
        storedMessage.UpdatedAt = DateTime.Now;

        var updatedMessage = await this._messageRepository.UpdateAsync(storedMessage);

        var clientUserId = storedMessage.Chat.UserId;

        var currentUserClient = await this._userRepository.SelectByIdAsync(clientUserId);

        await this._chatHubContext.Clients.Client(currentUserClient.ConnectionId).SendAsync("OnMessageStatusChanged", storedMessage);
        await this._chatHubContext.Clients.Group("Admins").SendAsync("OnMessageStatusChanged", storedMessage);

        return storedMessage;
    }
}
