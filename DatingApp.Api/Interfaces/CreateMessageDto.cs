namespace DatingApp.Api.Interfaces;

public class CreateMessageDto
{
    public required Guid RecipientUsername { get; set; }
    public required string Content { get; set; }
}