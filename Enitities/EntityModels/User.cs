﻿using Microsoft.EntityFrameworkCore;
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

    [Column("email")]
    [MaxLength(50)]
    public string Email { get; set; }

    [Column("is_moderator")]
    public bool IsModerator { get; set; }

    [Column("connection_id")]
    public string ConnectionId { get; set; }
}
