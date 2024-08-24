using DatingApp.Domain.Aggregates.UserProfileAggregates;

namespace DatingApp.Application.Likes.Dtos;

public class UserLikeDto
{
    public UserProfile SourceUser { get; set; } = null!;
    public Guid SourceUserId { get; set; }
    public UserProfile TargetUser { get; set; } = null!;
    public Guid TargetUsedId { get; set; }
}