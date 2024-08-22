namespace DatingApp.Application.UserProfiles.Helper;

public class UserParams
{
    private const int MaxPageSize = 50;
    public int PageNumber { get; set; } = 1;
    private int _pageSize = 10;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? PageSize : value;
    }

    public string? Gender { get; set; }
    public Guid? UserProfileIdToExclude { get; set; }
    public int MinAge { get; set; } = 18;
    public int MaxAge { get; set; } = 80;
    public string OrderBy { get; set; } = "lastActive";
}