using System.ComponentModel.DataAnnotations.Schema;
using DatingApp.Domain.Validators.UserProfileValidator;

namespace DatingApp.Domain.Aggregates.UserProfileAggregates;

public class Photos
{
    private Photos()
    {
        
    }
    public int Id { get; set; }
    public required string Url { get; set; }
    public bool IsMain { get; set; }

    public static Photos CreatePhoto(int id, string url, bool isMain)
    {
        var validator = new PhotoValidator();
        var ObjToValidate = new Photos
        {
            Id = id,
            Url = url,
            IsMain = isMain
        };

        var validationResult = validator.Validate(ObjToValidate);
        if (validationResult.IsValid) return ObjToValidate;

        return ObjToValidate;

    }
    
}