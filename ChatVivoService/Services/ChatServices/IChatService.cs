using Enitities.EntityModels;

namespace ChatVivoService.Services.ChatServices;

public interface IChatService
{
    Task<Chat> CreateChatAsync(string chatName, int userId);
    IQueryable<Chat> GetChatsByUserId(int userId);
    Task<Chat> DeleteChatAsync(int chatId);
    Task<Chat> UpdateChatStatusAsync(int chatId);
}
