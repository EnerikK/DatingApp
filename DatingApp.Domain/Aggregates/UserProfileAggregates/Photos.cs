using System.ComponentModel.DataAnnotations.Schema;

namespace DatingApp.Domain.Aggregates.UserProfileAggregates;

[Table("Photos")]
public class Photos()
{
    public int Id { get; set; }
    public required string Url { get; set; }
    public bool IsMain { get; set; }
    public string PublicId { get; set; }

    public int BasicInfoId { get; set; }  // Foreign key
    public BasicInfo BasicInfo { get; set; }  // Navigation property
}