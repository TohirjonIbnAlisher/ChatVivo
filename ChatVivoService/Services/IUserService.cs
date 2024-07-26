using ChatVivoService.DataTransferObjects;
using Enitities.EntityModels;

namespace ChatVivoService.Services;

public interface IUserService
{
    Task<User> CreateUserAsync(UserCreationDto userCreationDto);
    Task<User> UpdateUserAsync(User user);
    Task DeleteUserAsync(User user);
}
