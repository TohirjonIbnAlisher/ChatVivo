using ChatVivoService.DataTransferObjects;
using Enitities.EntityModels;

namespace ChatVivoService.Services;

public interface IMessageService
{
    Task<Message> CreateMessageAsync(CreateMessageDTO userMessageDto);
    Task<Message> UpdateMessageStatusAsync(int messageId);
    Task<Message> UpdateMessageAsync(ModifyMessageDTO modifyMessageDTO);
    Task DeleteMessageAsync(Message message);

    IQueryable<Message> GetAllMessagesByUserId(int userId);
    IQueryable<Message> GetAllMessagesByChatId(int chatId);
}
