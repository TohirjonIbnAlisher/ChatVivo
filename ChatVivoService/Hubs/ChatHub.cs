using Enitities.Repositories.AdminRepositories;
using Enitities.Repositories.UserRepositories;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace ChatVivoService.Hubs;

public class ChatHub : Hub
{
    public IUserRepositoy _userRepository;
    public IAdminRepository _adminRepository;

    public ChatHub(
        IUserRepositoy _userRepository,
        IAdminRepository adminRepository)
    {
        this._userRepository = _userRepository;
        _adminRepository = adminRepository;
    }

    public override Task OnConnectedAsync()
    {
        return base.OnConnectedAsync();
    }

    public async override Task OnDisconnectedAsync(Exception exception)
    { 
        var storedAdmin = await this._adminRepository
            .SelectByExpressionAsync(
            admin => admin.ConnectionId == Context.ConnectionId,
            new string[] { }).
            FirstOrDefaultAsync();

        if (storedAdmin == null)
            return;

        await this.Groups.RemoveFromGroupAsync(Context.ConnectionId, "Moderators");
    }

    public async Task ReconnectUserAsync(int id, bool isModerator)
    {
        if(!isModerator)
        {
            var storedUser = await this._userRepository.SelectByIdAsync(id);
            if (storedUser == null)
                return;
            storedUser.ConnectionId = Context.ConnectionId;

            var upatedUser = await this._userRepository.UpdateAsync(storedUser);
        }

        else
        {
            var storedAdmin = await this._adminRepository.SelectByIdAsync(id);
            if (storedAdmin == null)
                return;
            storedAdmin.ConnectionId = Context.ConnectionId;

            var upateAdmin = await this._adminRepository.UpdateAsync(storedAdmin);

            await this.Groups.AddToGroupAsync(Context.ConnectionId, "Moderators");
        }
    }

}
