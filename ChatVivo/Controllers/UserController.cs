using ChatVivoService.DataTransferObjects;
using ChatVivoService.Services;
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
}
