using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ChatVivoService.DataTransferObjects.MessageDTOs;

public class CreationMessageFileDTO : MessageDTO
{
    [Required]
    public string Field { get; set; }

    [Required]
    public List<IFormFile> Files { get; set; }
}
