using ChatVivo.Helpers;
using ChatVivoService.DataTransferObjects.MessageDTOs;
using ChatVivoService.Services.FileServices;
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
        [FromForm] CreationMessageFileDTO model)
    {
        var path = FileHelper.AddFiles(model);

        var insertedPhoto = await _fileService.SendFilePathAsync(path, model as MessageDTO);

        return Ok(insertedPhoto);
    }
}
