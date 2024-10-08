using ChatVivoService.DataTransferObjects.MessageDTOs;
using Enitities.EntityModels;

namespace ChatVivoService.Services;

public interface IMessageService
{
    Task<Message> CreateMessageAsync(CreationMessageTextDTO userMessageDto);
    Task<Message> CreateMessageObjectAsync(Message message);
    Task<Message> UpdateMessageStatusAsync(int messageId);
    Task<Message> UpdateMessageAsync(ModifyMessageDTO modifyMessageDTO);
    Task DeleteMessageAsync(Message message);

    Task<IQueryable<Message>> GetAllMessagesByUserId(ParameterMessageDTO dto);
    IQueryable<Message> GetAllMessagesByChatId(int chatId);
}
