namespace ChatVivoService.DataTransferObjects;

public class UserCreationDto
{ 
    public string FIO {  get; set; }
    public string PhoneNumber { get; set; }
    public bool IsModerator { get; set; } = false;
    public string ConnectionId { get; set; }
}
