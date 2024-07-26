using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enitities.EntityModels;

[Table("user",Schema = "auth")]
[Index(nameof(PhoneNumber), IsUnique = true)]
public class User : BaseModel
{
    [Column("fist_name")]
    [MaxLength(125)]
    public string FistName { get; set; }

    [Column("last_name")]
    [MaxLength(125)]
    public string? LastName { get; set; }

    [Column("phone_number")]
    [MaxLength(30)]
    public string PhoneNumber { get; set; }

    [Column("token")]
    public string? Token { get; set; }
    [Column("token_expired_date")]
    public DateTime? TokenExpiedDate { get; set; }
}
