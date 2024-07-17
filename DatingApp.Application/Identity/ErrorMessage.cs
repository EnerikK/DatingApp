﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Identity
{
    public class ErrorMessages
    {
        public const string NoExistingUser = "Unable to find the user with this username";
        public const string IncorrectPassword = "The provided password is incorrect";
        public const string UserAlreadyExists = "The email address is already in use , cannot register new user";
        public const string UnauthorizedAccountRemoval = "Cannot remove account as you are not its owner";
    }
}
