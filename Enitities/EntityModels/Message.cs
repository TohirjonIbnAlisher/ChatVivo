using System.ComponentModel.DataAnnotations.Schema;

namespace Enitities.EntityModels;

[Table("message", Schema = "chats")]
public class Message : BaseModel
{
    [Column("text")]
    public string Text { get; set; }
    [Column("chat_id")]
    [ForeignKey(nameof(Chat))]
    public int ChatId { get; set; }
    public virtual Chat Chat { get; set; }

    [Column("sender_id")]
    [ForeignKey(nameof(Sender))]
    public int SenderId { get; set; }
    public virtual User Sender { get; set; }

    [Column("is_read")]
    public bool IsRead { get; set; } = false;

    [Column("sent_datetime")]
    public DateTime SentDateTime { get; set; } = DateTime.Now;
}
