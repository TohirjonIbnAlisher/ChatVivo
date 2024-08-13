using Enitities.EntityModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatVivoService.DataTransferObjects;

public class AdminCreationDTO : UserCreationDto
{
    public bool IsBlocked { get; set; } = false;

    public AdminType Type { get; set; } = AdminType.Admin;
}
