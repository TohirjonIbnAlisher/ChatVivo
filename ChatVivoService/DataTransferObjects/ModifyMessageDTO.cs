namespace ChatVivoService.DataTransferObjects;

public class ModifyMessageDTO
{
    public int Id { get; set; }
    public string Message { get; set; }
    
    public bool? IsRead { get; set; }
}
