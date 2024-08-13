using ChatVivoService.DataTransferObjects;
using Enitities.EntityModels;

namespace ChatVivoService.Services.AdminServices;

public interface IAdminService
{
    Task<Admin> CreateAdminAsync(AdminCreationDTO adminCreationDTO);
    Task<Admin> UpdateAdminStatusAsync(int adminId);
}
