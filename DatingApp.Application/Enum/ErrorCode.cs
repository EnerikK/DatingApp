﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Enum
{
    public enum ErrorCode
    {
        NotFound = 404,
        ServerError = 500,
        
        IdentityUserDoesNotExist = 203,
        PhotoUploadFailed = 204,
        InvalidPhotoId = 205,
        
        UnauthorizedAccountRemoval = 303,
        
        IdentityUserAlreadyExists = 501,
        IdentityCreationFailed = 502,
        UserNotFound = 503,
        MessageNotFound = 504,
        TargetIdAndSourceIdAreTheSame = 505,

        UnknownError = 1

    }
}
