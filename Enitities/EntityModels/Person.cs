using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Enitities.EntityModels;

[Index(nameof(PhoneNumber), IsUnique = true)]
public class Person : BaseModel
{
    [Column("fio")]
    [MaxLength(125)]
    public string FIO { get; set; }

    [Column("phone_number")]
    [MaxLength(30)]
    public string PhoneNumber { get; set; }

    [Column("connection_id")]
    public string ConnectionId { get; set; }
}
