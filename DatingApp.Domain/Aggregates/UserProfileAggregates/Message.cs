namespace DatingApp.Domain.Aggregates.UserProfileAggregates;

public class Message
{
    public int Id { get; set; }
    public required string SenderUsername { get; set; }
    public required string RecipientUsername  { get; set; }
    public required string Content { get; set; }
    public DateTime DateRead { get; set; }
    public DateTime MessageSent { get; set; } = DateTime.UtcNow;
    public bool SenderDeleted { get; set; }
    public bool RecipientDeleted { get; set; }
    
    //navigation props
    public Guid SenderId { get; set; }
    public UserProfile Sender { get; set; } = null!;
    public Guid RecipientId { get; set; }
    public UserProfile Recipient { get; set; } = null!;
}