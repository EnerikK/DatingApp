namespace DatingApp.Api.Interfaces;

public class MessageDto
{
    public int Id { get; set; }
    public Guid SenderGuid { get; set; }   
    public required string SenderUsername { get; set; }
    public required string SenderPhotoUrl { get; set; }
    public Guid RecipientGuid { get; set; }
    public required string RecipientUsername  { get; set; }
    public required string RecipientPhotoUrl { get; set; }
    public required string Content { get; set; }
    public DateTime DateRead { get; set; }
    public DateTime MessageSent { get; set; }
    
}