using ChatVivo.Helpers;
using ChatVivoService.Services.FileServices;
using Enitities.FileModels;
using Microsoft.AspNetCore.Mvc;

namespace ChatVivo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FileController : ControllerBase
{
    private readonly FileService _fileService;

    public FileController(
        FileService _fileService)
    {
        this._fileService = _fileService;
    }

    [HttpPost]
    public async Task<IActionResult> AddFileAsync(
        [FromForm] AddFileModels model)
    {
        var path = FileHelper.AddFiles(model);

        await _fileService.SendFilePathAsync(path, model.UserFileHelper);

        return Ok(path);
    }
}
