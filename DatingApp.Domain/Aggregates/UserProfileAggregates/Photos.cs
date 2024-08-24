using System.ComponentModel.DataAnnotations.Schema;
using DatingApp.Domain.Validators.UserProfileValidator;

namespace DatingApp.Domain.Aggregates.UserProfileAggregates;

[Table("Photo")]
public class Photos
{
    public int Id { get; set; }
    public string? Url { get; set; }
    public bool IsMain { get; set; }
    
}