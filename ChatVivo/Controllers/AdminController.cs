using Microsoft.AspNetCore.Mvc;

namespace ChatVivo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private IAdminService _adminService;

    public AdminController(IAdminService AdminService)
    {
        _AdminService = AdminService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAdminAsync([FromBody] AdminCreationDto dto)
    {
        var createdAdmin = await _AdminService.CreateAdminAsync(dto);
        return Ok(createdAdmin);
    }

    [HttpPut]
    public async Task<IActionResult> ModifyAdminStatusAsync(int id)
    {
        var createdAdmin = await _AdminService.CreateAdminAsync(dto);
        return Ok(createdAdmin);
    }

    //[HttpGet("Id")]
    //public async Task<ActionResult<Admin>> GetAdminByIdAsync([FromQuery] int id)
    //{
    //    return await this._AdminService.GetAdminByIdAsync(id);
    //}

    //[HttpGet]
    //public ActionResult<IQueryable<Admin>> GetAllAdmins()
    //{
    //    var allAdmins = this._AdminService.GetAllAdmins();

    //    return Ok(allAdmins);
    //}

    //[HttpDelete("Id")]
    //public async Task<ActionResult<Admin>> DeleteAdminAsync(int id)
    //{
    //    return await this._AdminService.DeleteAdminAsync(id);
    //}
}
