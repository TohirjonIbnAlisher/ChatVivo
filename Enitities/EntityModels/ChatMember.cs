using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enitities.EntityModels;

[Table("chat_member", Schema = "chats")]
public class ChatMember : BaseModel
{
    [Column("user_id")]
    [ForeignKey("user_chat_member_id")]
    public int UserId { get; set; }
    public virtual User User { get; set; }
    [Column("chat_id")]
    [ForeignKey("chat_chat_member_id")]
    public int ChatId { get; set; }
    public virtual Chat Chat { get; set; }
}
