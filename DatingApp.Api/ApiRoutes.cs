namespace DatingApp.Api
{
    public class ApiRoutes
    {
        public const string BaseRoute = "api/v{version:apiversion}/[controller]";
        public static class UserProfiles
        {
            public const string IdRoute = "{id}";
            public const string AddPhoto = "AddPhoto";
            public const string SetPhotoMain = "SetPhotoMain";
            public const string DeletePhoto = "DeletePhoto";
        }
        public static class Identity
        {
            public const string Login = "login";
            public const string Registration = "registration";
            public const string IdentityById = "{identityUserId}";
            public const string CurrentUser = "currentuser";
        }

    }
}
