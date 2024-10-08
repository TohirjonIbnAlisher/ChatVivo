namespace ChatVivoService.DataTransferObjects.MessageDTOs;

public class MessageDTO
{
    public int ChatId { get; set; }
    public int? ParentId { get; set; }
    public int SenderId { get; set; }
}
