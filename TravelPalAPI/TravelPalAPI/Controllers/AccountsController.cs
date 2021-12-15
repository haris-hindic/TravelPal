using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TravelPalAPI.Database;
using TravelPalAPI.Models;
using TravelPalAPI.ViewModels.Identity;


namespace TravelPalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly AppDbContext appDb;
        private readonly UserManager<UserAccount> userManager;
        private readonly SignInManager<UserAccount> signInManager;
        private readonly IConfiguration configuration;

        public AccountsController(AppDbContext appDb,UserManager<UserAccount> userManager,
            SignInManager<UserAccount> signInManager,IConfiguration configuration)
        {
            this.appDb = appDb;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

        [HttpPost("signIn")]
        public async Task<ActionResult<AuthentificationResponse>> SignIn([FromBody] LogInUserCredentials userCredentials)
        {
            var result = await signInManager.PasswordSignInAsync(userCredentials.UserName, 
                userCredentials.Password, isPersistent: false, lockoutOnFailure: false);

            var user = await userManager.FindByNameAsync(userCredentials.UserName);

            if (result.Succeeded)
                return BuildToken(user);
            else
                return BadRequest("Incorrect Login");
        }


        [HttpPost("signUp")]
        public async Task<ActionResult<AuthentificationResponse>> SignUp([FromBody]UserCredentials userCredentials)
        {
            var user = new UserAccount
            {
                UserName = userCredentials.UserName,
                Email = userCredentials.Email,
                FirstName = userCredentials.FirstName,
                LastName = userCredentials.LastName,
                PhoneNumber = userCredentials.PhoneNumber
            };
            var result = await userManager.CreateAsync(user, userCredentials.Password);

            if (result.Succeeded)
                return BuildToken(user);
            else
                return BadRequest(result.Errors);
        }

        private AuthentificationResponse BuildToken(UserAccount user)
        {
            var claims = new List<Claim>()
            {
                new Claim("userName",user.UserName),
                new Claim("id",user.Id)
            };

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
