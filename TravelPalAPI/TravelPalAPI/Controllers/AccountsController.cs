using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
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
using TravelPalAPI.Helpers;
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
        private readonly IMapper mapper;
        private readonly ITokenService tokenService;
        private readonly string claimType="role";
        private readonly string claimValue="admin";
        private IConfiguration configuration;


        public AccountsController(AppDbContext appDb,UserManager<UserAccount> userManager,
            SignInManager<UserAccount> signInManager,IMapper mapper,ITokenService tokenService,
            IConfiguration configuration)
        {
            this.appDb = appDb;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
            this.tokenService = tokenService;
            this.configuration = configuration;
        }

        [HttpGet("users"),Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme,Policy ="IsAdmin")]
        public async Task<ActionResult<List<UserVM>>> GetUsers()
        {
            var users = await appDb.UserAccounts.OrderBy(x => x.UserName).ToListAsync();

            var usersVM = mapper.Map<List<UserVM>>(users);

            var x = appDb.UserClaims.ToList();

            foreach (var user in usersVM)
            {
                user.StaysListed = appDb.Accommodations.Where(x => x.HostId == user.Id).Count();

                user.EventsListed = appDb.Events.Where(e => e.HostId == user.Id).Count();

                user.IsAdmin = appDb.UserClaims
                    .Any(x => x.UserId == user.Id && x.ClaimType == claimType && x.ClaimValue == claimValue);
            }

            return usersVM;
        }

        [HttpPost("makeAdmin"), Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
        public async Task<ActionResult> MakeAdmin([FromBody] string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            await userManager.AddClaimAsync(user, new Claim("role", "admin"));
            return NoContent();
        }

        [HttpPost("removeAdmin"), Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
        public async Task<ActionResult> RemoveAdmin([FromBody] string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            await userManager.RemoveClaimAsync(user, new Claim("role", "admin"));
            return NoContent();
        }

        [HttpPost("signIn")]
        public async Task<ActionResult<AuthentificationResponse>> SignIn([FromBody] LogInUserCredentials userCredentials)
        {
            var user = await userManager.FindByNameAsync(userCredentials.UserName);


            var result = await signInManager.PasswordSignInAsync(userCredentials.UserName, 
                userCredentials.Password, isPersistent: false, lockoutOnFailure: false);



            if (result.Succeeded)
            {
                if (!await userManager.IsEmailConfirmedAsync(user))
                    return Unauthorized("Email is not confirmed");
                return await tokenService.BuildToken(user);
            }
            else
                return BadRequest("Incorrect Login");


        }


        [HttpPost("signUp")]
        public async Task<ActionResult<AuthentificationResponse>> SignUp([FromBody]UserCredentials userCredentials)
        {
            if(appDb.Users.Any(x=>x.Email == userCredentials.Email))
            {
                return BadRequest("Email already exist");
            }


            var user = new UserAccount
            {
                UserName = userCredentials.UserName,
                Email = userCredentials.Email,
                FirstName = userCredentials.FirstName,
                LastName = userCredentials.LastName,
                PhoneNumber = userCredentials.PhoneNumber,
                ClientURI = userCredentials.ClientURI
            };
            var result = await userManager.CreateAsync(user, userCredentials.Password);

            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

            var info = new Dictionary<string, string>
            {
                {"token", token },
                {"email", user.Email }
            };

            var callback = QueryHelpers.AddQueryString(user.ClientURI, info);

            if (result.Succeeded)
            {
                EmailSettings.SendEmail(configuration, "Test", user.Email, "Confirmation",
                    $"Welcome {user.FirstName} {user.LastName} Just one more step before you get to the fun part.\n"
                   +$"Confirm your email address with this link:\n\n" +
                   $"{callback}");
                return await tokenService.BuildToken(user); }
            else
                return BadRequest(result.Errors);

            return Ok();
        }
        [HttpGet("emailconfirmation")]
        public async Task<IActionResult> EmailConfirmation([FromQuery] string email, [FromQuery] string token)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest();

            var confirmResult = await userManager.ConfirmEmailAsync(user, token);

            if (!confirmResult.Succeeded)
                return BadRequest();

            return Ok();
        }
    }
}
