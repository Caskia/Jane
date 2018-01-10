using System;
using Microsoft.IdentityModel.Tokens;

namespace Jane.AspNetCore.Authentication.JwtBearer
{
    public class TokenAuthConfiguration
    {
        public string Audience { get; set; }

        public TimeSpan Expiration { get; set; }

        public string Issuer { get; set; }

        public SecurityKey SecurityKey { get; set; }

        public SigningCredentials SigningCredentials { get; set; }
    }
}