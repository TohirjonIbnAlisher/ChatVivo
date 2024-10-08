namespace ChatVivoService.DataTransferObjects.MessageDTOs;

public class ParameterMessageDTO
{
    public int UserId {  get; set; }
    public bool IsModerator { get; set; }
    public int Page { get; set; }
    public int Count { get; set; }
}
