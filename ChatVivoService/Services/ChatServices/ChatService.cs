using ChatVivoService.Hubs;
using Enitities.EntityModels;
using Enitities.Repositories.ChatMemberRepository;
using Enitities.Repositories.ChatRepositories;
using Microsoft.AspNetCore.SignalR;

namespace ChatVivoService.Services.ChatServices;

public class ChatService : IChatService
{
    private readonly IChatRepository _chatRepository;
    private readonly IHubContext<ChatHub> _chatHubContext;
    private readonly IUserService _userService;

    public ChatService(
        IChatRepository _chatRepository,
        IHubContext<ChatHub> chatHubContext,
        IUserService _userService)
    {
        this._chatRepository = _chatRepository;
        this._chatHubContext = chatHubContext;
        this._userService = _userService;
    }

    public async Task<Chat> CreateChatAsync(string chatName, int userId)
    {
        var chat = new Chat()
        {
            CreatedAt = DateTime.Now,
            Name = chatName,
            UserId = userId
        };
        var storedChat = await this._chatRepository.InsertAsync(chat);

        var storedUser = await this._userService.GetUserByIdAsync(userId);

        await _chatHubContext.Clients.Client(storedUser.ConnectionId).SendAsync("OnCreatedNewTheme", storedChat);

        await _chatHubContext.Clients.Group("Admin").SendAsync("OnCreatedNewTheme", storedChat);

        return storedChat;
    }

    public async Task<Chat> DeleteChatAsync(int chatId)
    {
        var selectedChat = await this._chatRepository.SelectByIdAsync(chatId);

        if(selectedChat == null)
        {
            throw new Exception($"Chat not found with id {chatId}");
        }

        var deletedChat = await this._chatRepository.DeleteAsync(selectedChat);

        return deletedChat;

    }

    public IQueryable<Chat> GetChatsByUserId(int userId)
    {
        return this._chatRepository.SelectByExpressionAsync(chat => chat.UserId == userId, new string[] {"User"});
    } 
}
