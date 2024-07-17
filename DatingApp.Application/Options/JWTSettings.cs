using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Options
{
    public class JWTSettings
    {
        public string SigningKey { get; set; }
        public string Issuer { get; set; }
        public string[] Audience { get; set; }
    }
}

