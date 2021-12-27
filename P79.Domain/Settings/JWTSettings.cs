using System;
using System.Collections.Generic;
using System.Text;

namespace P79.Domain.Settings
{
    public class JWTSettings
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double ExpiryTimeInSeconds { get; set; }
    }
}
