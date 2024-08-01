namespace ChatVivoService.DataTransferObjects;

public class CreateMessageDTO
{
    public string Message { get; set; }
    public int ChatId { get; set; }
    public int? ParentId { get; set; }
    public int SenderId { get; set; }
}
