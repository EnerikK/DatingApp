using System.ComponentModel.DataAnnotations.Schema;
using DatingApp.Domain.Validators.UserProfileValidator;

namespace DatingApp.Domain.Aggregates.UserProfileAggregates;

[Table("Photo")]
public class Photos
{
    public int Id { get; set; }
    public string? Url { get; set; }
    public bool IsMain { get; set; }
    
    /*// Reference to the BasicInfo to notify when IsMain changes
    public BasicInfo BasicInfo { get; set; }

    public void SetAsMain()
    {
        if (!IsMain)
        {
            IsMain = true;
            BasicInfo?.UpdatePhotoUrl(Url);  // Update PhotoUrl in BasicInfo
        }
    }

    public void UnsetMain()
    {
        if (IsMain)
        {
            IsMain = false;
        }
    }*/
}