using ChatVivoService.DataTransferObjects;
using ChatVivoService.Services;
using Enitities.EntityModels;
using Microsoft.AspNetCore.Mvc;

namespace ChatVivo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUserAsync([FromBody] UserCreationDto dto)
    {
        await _userService.CreateUserAsync(dto);
        return Ok(dto);
    }

    [HttpGet("Id")]
    public async Task<ActionResult<User>> GetUserByIdAsync([FromQuery] int id)
    {
        return await this._userService.GetUserByIdAsync(id);
    }

    [HttpDelete("Id")]
    public async Task<ActionResult<User>> DeleteUserAsync(int id)
    {
        return await this._userService.DeleteUserAsync(id);
    }

}
