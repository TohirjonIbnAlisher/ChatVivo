namespace ChatVivoService.DataTransferObjects;

public class CreateMessageDTO
{
    public int Id { get; set; }
    public string Message { get; set; }
    public int ChatId { get; set; }
    public int SenderId { get; set; }
}
