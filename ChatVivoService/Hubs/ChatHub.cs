using Enitities.Repositories.UserRepositories;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace ChatVivoService.Hubs;

public class ChatHub : Hub
{
    private int id = 0;
    public IUserRepositoy _userRepository;

    public ChatHub(IUserRepositoy _userRepository)
    {
        this._userRepository = _userRepository;
    }

    public override Task OnConnectedAsync()
    {
        return base.OnConnectedAsync();
    }

    public async override Task OnDisconnectedAsync(Exception exception)
    {
        var storedUser = await this._userRepository
            .SelectByExpressionAsync(
            user => user.ConnectionId == Context.ConnectionId,
            new string[] { }).
            FirstOrDefaultAsync();

        if (storedUser == null)
            return;

        var deletedUser = await this._userRepository.DeleteAsync(storedUser);

        await Clients.Group("Admin").SendAsync("OnDeleteUser", deletedUser);   
    }

    public async Task ReconnectUserAsync(int userId)
    {
        var storedUser = await this._userRepository.SelectByIdAsync(userId + (id ++));

        if (storedUser == null)
            return;

        storedUser.ConnectionId = Context.ConnectionId;

        var upatedUser = await this._userRepository.UpdateAsync(storedUser);

        if (storedUser.IsModerator)
        {
            await this.Groups.AddToGroupAsync(Context.ConnectionId, "Admin");
        }
    }

}
