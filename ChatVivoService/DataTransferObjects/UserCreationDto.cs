namespace ChatVivoService.DataTransferObjects;

public class UserCreationDto
{ 
    public string FirstName {  get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsModerator { get; set; }   
    public string ConnectionId { get; set; }
}
