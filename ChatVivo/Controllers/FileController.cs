using Microsoft.AspNetCore.Mvc;

namespace ChatVivo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FileController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddFileAsync(IFormFile formFile)
    {

    }
}
