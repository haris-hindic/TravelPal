using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TravelPalAPI.Models;
using TravelPalAPI.ViewModels.Identity;

namespace TravelPalAPI.Helpers
{
    public class TokenService : ITokenService
     
    {
        private readonly UserManager<UserAccount> userManager;
        private readonly IConfiguration configuration;

        public TokenService(UserManager<UserAccount> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

        public async Task<AuthentificationResponse> BuildToken(UserAccount user)
        {
            var claims = new List<Claim>()
            {
                new Claim("userName",user.UserName),
                new Claim("id",user.Id)
            };

            var claimsDB = await userManager.GetClaimsAsync(user);

            claims.AddRange(claimsDB);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["keyjwt"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(5);

            var token = new JwtSecurityToken(
                issuer: null, audience: null, claims: claims,
                expires: expiration, signingCredentials: creds);

            return new AuthentificationResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
