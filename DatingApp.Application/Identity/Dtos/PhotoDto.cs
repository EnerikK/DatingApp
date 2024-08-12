using DatingApp.Domain.Aggregates.UserProfileAggregates;

namespace DatingApp.Application.Identity.Dtos;

public class PhotoDto
{
    public string Id { get; set; }
    public string Url { get; set; }
    public bool IsMain { get; set; }

}