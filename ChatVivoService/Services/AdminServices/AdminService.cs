using ChatVivoService.DataTransferObjects;
using ChatVivoService.Hubs;
using Enitities.EntityModels;
using Enitities.Repositories.AdminRepositories;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace ChatVivoService.Services.AdminServices;

public class AdminService : IAdminService
{
    private IAdminRepository _adminRepository;
    private readonly IHubContext<ChatHub> _hubContext;


    public AdminService(IAdminRepository adminRepository,
        IHubContext<ChatHub> hubContext)
    {
        this._adminRepository = adminRepository;
        _hubContext = hubContext;
    }

    public async Task<Admin> CreateAdminAsync(AdminCreationDTO adminCreationDTO)
    {
        var storedAdmin = await this._adminRepository.SelectByExpressionAsync(admin => admin.PhoneNumber == adminCreationDTO.PhoneNumber, new string[] {}).FirstOrDefaultAsync();

        if (storedAdmin != null)
        {
            throw new Exception("Admin already found");
        }

        Admin admin = new Admin()
        {
            ConnectionId = adminCreationDTO.ConnectionId,
            PhoneNumber = adminCreationDTO.PhoneNumber,
            FIO = adminCreationDTO.FIO,
            IsBlocked = false,
            Type = AdminType.Admin,
            CreatedAt = DateTime.Now
        };

        var insertedAdmin = await this._adminRepository.InsertAsync(admin);

        await this._hubContext.Groups.AddToGroupAsync(insertedAdmin.ConnectionId, "Moderators");

        return insertedAdmin;

    }

    public async Task<Admin> UpdateAdminStatusAsync(int adminId)
    {
        var storedAdmin = await this._adminRepository.SelectByIdAsync(adminId);

        if (storedAdmin == null)
        {
            throw new Exception("Admin not found");
        }


        storedAdmin.IsBlocked = true;

        return storedAdmin;
    }
}
