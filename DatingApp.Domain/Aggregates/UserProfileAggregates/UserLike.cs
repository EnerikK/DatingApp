namespace DatingApp.Domain.Aggregates.UserProfileAggregates;

public class UserLike
{
    public UserProfile? SourceUser { get; set; } 
    public Guid SourceUserId { get; set; }
    public UserProfile? TargetUser { get; set; } 
    public Guid TargetUserId { get; set; }
    
}