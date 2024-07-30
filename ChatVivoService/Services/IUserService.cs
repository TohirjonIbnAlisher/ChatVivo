using ChatVivoService.DataTransferObjects;
using Enitities.EntityModels;

namespace ChatVivoService.Services;

public interface IUserService
{
    Task<User> CreateUserAsync(UserCreationDto userCreationDto);
    Task<User> UpdateUserAsync(UserModifyDto user);
    Task<User> DeleteUserAsync(int userId);
    IQueryable<User> GetAllUsers();

    Task<User> GetUserByIdAsync(int userId);
}
