using System.ComponentModel.DataAnnotations.Schema;
using DatingApp.Domain.Validators.UserProfileValidator;

namespace DatingApp.Domain.Aggregates.UserProfileAggregates;

[Table("Photo")]
public class Photos
{
    public int Id { get; set; }
    public string Url { get; set; }
    public bool IsMain { get; set; }
    public static Photos CreatePhoto(int id, string url, bool isMain)
    {
        var photo = new Photos();
        photo.Id = id;
        photo.Url = url;
        photo.IsMain = isMain;

        return photo;
    }
}