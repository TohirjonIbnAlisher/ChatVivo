using ChatVivoService.DataTransferObjects;
using ChatVivoService.Services.AdminServices;
using Microsoft.AspNetCore.Mvc;

namespace ChatVivo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private IAdminService _adminService;

    public AdminController(IAdminService AdminService)
    {
        this._adminService = AdminService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAdminAsync([FromBody] AdminCreationDTO dto)
    {
        var createdAdmin = await _adminService.CreateAdminAsync(dto);
        return Ok(createdAdmin);
    }

    [HttpPut]
    public async Task<IActionResult> ModifyAdminStatusAsync(int id)
    {
        var updatedAdmin = await this._adminService.UpdateAdminStatusAsync(id);
        return Ok(updatedAdmin);
    }
}
