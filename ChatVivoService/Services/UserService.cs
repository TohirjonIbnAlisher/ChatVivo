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
        var storedUser = this._userRepository.SelectByExpressionAsync(user => user.PhoneNumber == userCreationDto.PhoneNumber, new string[] { });

        if(storedUser != null)
        {
            throw new Exception("User already Exist");
        }

        User user = new User()
        {
            FistName = userCreationDto.FirstName,
            LastName = userCreationDto.LastName,
            CreatedAt = DateTime.Now,
            PhoneNumber = userCreationDto.PhoneNumber,
            IsModerator = userCreationDto.IsModerator,
            ConnectionId = userCreationDto.ConnectionId,
            Token = "",
            TokenExpiedDate = null
        };


        var insertedUser = await this._userRepository.InsertAsync(user);

        if (userCreationDto.IsModerator)
        {
            await this._hubContext.Groups.AddToGroupAsync(userCreationDto.ConnectionId, "Admins");
        }

        else
        {
            await this._hubContext.Clients.Group("Admin").SendAsync("OnCreatedNewUser", insertedUser);
        }

        return user;

    }

    public async Task<User> DeleteUserAsync(int userId)
    {
        var storedUser = await this._userRepository.SelectByExpressionAsync(user => user.Id == user.Id, new string[] { }).FirstOrDefaultAsync();

        if (storedUser == null)
        {
            throw new Exception("User does not Exist");
        }

        var deletedUser = await this._userRepository.DeleteAsync(storedUser);

        if (storedUser.IsModerator)
        {
            await this._hubContext.Groups.RemoveFromGroupAsync(storedUser.ConnectionId, "Admins");
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
        storedUser.FistName = user.FirstName ?? storedUser.FistName;
        storedUser.LastName = user.LastName ?? storedUser.LastName;
        storedUser.LastName = user.LastName ?? storedUser.LastName;
        storedUser.UpdatedAt = DateTime.Now;

        var uptadetUser = await this._userRepository.UpdateAsync(storedUser);

        return uptadetUser;
    }      
    
}
