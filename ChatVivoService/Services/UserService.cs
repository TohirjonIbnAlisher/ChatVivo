using ChatVivoService.DataTransferObjects;
using ChatVivoService.Hubs;
using Enitities.EntityModels;
using Enitities.Repositories.UserRepositories;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace ChatVivoService.Services;

public class UserService : IUserService
{ 
    private readonly IUserRepositoy _userRepository;
    private readonly IHubContext<ChatHub> _hubContext;

    public UserService(
        IUserRepositoy userRepository,
        IHubContext<ChatHub> hubContext)
    {
        _userRepository = userRepository;
        _hubContext = hubContext;
    }

    public async Task<User> CreateUserAsync(UserCreationDto userCreationDto)
    {
        var storedUser = await this._userRepository.SelectByExpressionAsync(user => user.PhoneNumber == userCreationDto.PhoneNumber, new string[] { }).FirstOrDefaultAsync();

        if(storedUser != null)
        {
            throw new Exception("User already Exist");
        }

        User user = new User()
        {
            FIO = userCreationDto.FIO,
            CreatedAt = DateTime.Now,
            PhoneNumber = userCreationDto.PhoneNumber,
            IsModerator = userCreationDto.IsModerator,
            ConnectionId = userCreationDto.ConnectionId
        };


        var insertedUser = await this._userRepository.InsertAsync(user);

        if (userCreationDto.IsModerator)
        {
            await this._hubContext.Groups.AddToGroupAsync(userCreationDto.ConnectionId, "Admin");
        }

        else
        {
            //await this._hubContext.Clients.Client("iH94wd8WT9r_nkxyvYO0Gg").SendAsync("OnCreatedNewUser", insertedUser);
            await this._hubContext.Clients.Group("Admin").SendAsync("OnCreatedNewUser", insertedUser);
        }

        //var moderators = this._userRepository.SelectByExpressionAsync(user => user.IsModerator, new string[] { }).Select(user => user.ConnectionId);

        //await this._hubContext.Clients.Client("Oo-s-Wlvh7lcI2_o4buiYw").SendAsync("OnCreatedNewUser", "jgvgvg");

        return insertedUser;

    }

    public async Task<User> DeleteUserAsync(int userId)
    {
        var storedUser = await this._userRepository.SelectByExpressionAsync(user => user.Id == userId, new string[] { }).FirstOrDefaultAsync();

        if (storedUser == null)
        {
            throw new Exception("User does not Exist");
        }

        var deletedUser = await this._userRepository.DeleteAsync(storedUser);

        if (storedUser.IsModerator)
        {
            await this._hubContext.Groups.RemoveFromGroupAsync(storedUser.ConnectionId, "Admin");
        }

        else
        {
            await this._hubContext.Clients.Group("Admin").SendAsync("OnDeleteUser", deletedUser);
        }

        return deletedUser;
    }

    public IQueryable<User> GetAllUsers() 
        => this._userRepository.SelectAll();

    public async Task<User> GetUserByIdAsync(int userId)
    {
        var userById = await this._userRepository.SelectByIdAsync(userId);

        return userById;
    }

    public async Task<User> UpdateUserAsync(UserModifyDto user)
    {
        var storedUser = await this._userRepository.SelectByExpressionAsync(user => user.Id == user.Id, new string[] { }).FirstOrDefaultAsync();

        if (storedUser == null)
        {
            throw new Exception("User does not Exist");
        }

        storedUser.PhoneNumber = user.PhoneNumber ?? storedUser.PhoneNumber;
        storedUser.FIO = user.FirstName ?? storedUser.FIO;
       
        storedUser.UpdatedAt = DateTime.Now;

        var uptadetUser = await this._userRepository.UpdateAsync(storedUser);

        return uptadetUser;
    }      
}
