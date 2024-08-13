using System.ComponentModel.DataAnnotations.Schema;

namespace Enitities.EntityModels;

[Table("chat", Schema = "chats")]
public class Chat : BaseModel
{
    [Column("name")] public string Name { get; set; }

    [Column("user_id")] public int UserId { get; set; }

    public virtual User User { get; set; }

    [Column("status")] public ChatStatus Status { get; set; } = ChatStatus.Active;
}

public enum ChatStatus
{
    Active,
    Closed
}
