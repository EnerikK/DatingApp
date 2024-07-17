using System;
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
        
        
        IdentityUserAlreadyExists = 501,
        IdentityCreationFailed = 502,

        UnknownError = 1

    }
}
