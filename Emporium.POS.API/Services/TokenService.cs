using Emporium.POS.API.Configuration;
using Emporium.POS.API.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Emporium.POS.API.Services
{
    public class TokenService : ITokenService
    {
        private readonly ILogger logger;

        public TokenService(ILogger logger)
        {
            this.logger = logger;
        }

        public string GetToken()
        {
            try
            {
                logger.LogInformation("Getting token");

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(APISettings.AuthSecret));
                var expirationDate = new DateTimeOffset(DateTime.Now.AddDays(APISettings.TokenValidityDays).AddMinutes(APISettings.TokenValidityMinutes)).ToUnixTimeSeconds();
                var currentDate = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();

                var claims = new Claim[] {
                    new Claim(JwtRegisteredClaimNames.Exp, $"{expirationDate}"),
                    new Claim(JwtRegisteredClaimNames.Nbf, $"{currentDate}")
                };

                var token = new JwtSecurityToken(new JwtHeader(new SigningCredentials(key, SecurityAlgorithms.HmacSha256)), new JwtPayload(claims));
                var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

                logger.LogInformation("Returning token");

                return jwtToken;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to get token");
                throw;
            }
        }
    }
}
