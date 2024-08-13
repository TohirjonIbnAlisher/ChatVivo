using System.ComponentModel.DataAnnotations.Schema;

namespace Enitities.EntityModels;

[Table("admin", Schema = "auth")]
public class Admin : Person
{
    [Column("is_blocked")] public bool IsBlocked { get; set; } = false;

    [Column("type")] public AdminType Type { get; set; } = AdminType.Admin;
}

public enum AdminType
{
    Admin,
    SuperAdmin
}
