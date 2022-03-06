using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
using TravelPalAPI.Repositories.Implementation;
using TravelPalAPI.ViewModels.Identity;
using TravelPalAPI.ViewModels.User;

namespace TravelPalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        public IAccountRepository accountRepository;

        private IConfiguration configuration;
        public ITokenService tokenService;
        private readonly UserManager<UserAccount> userManager;
        private readonly string clientURI = "http://localhost:4200/authentication/emailconfirmation";
        private readonly IEmailSenderService emailService;




        public AccountsController(IAccountRepository accountRepository, UserManager<UserAccount> userManager, ITokenService tokenService, IConfiguration configuration, IEmailSenderService emailService)
        {
            this.accountRepository = accountRepository;
            this.tokenService = tokenService;
            this.userManager = userManager;
            this.emailService = emailService;
            this.configuration = configuration;
        }




        [HttpGet("emailconfirmation")]
        public async Task<IActionResult> EmailConfirmation([FromQuery] string email, [FromQuery] string token)
        {
            var result = accountRepository.EmailConfirmation(email, token);

            if (result == null)
                return NotFound();

            if (result.Result.Succeeded)
                return Ok();
            else
                return BadRequest(result.Result.Errors);


        }

        [HttpGet("sendForgotPassword")]
        public async Task<IActionResult> SendForgotPassword([FromQuery] string email)
        {
            await accountRepository.SendForgotPassword(email);
            return Ok();
           
        }

        [HttpPost("resetForgottenPass")]
        public async Task<ActionResult> ResetForgottenPassword([FromBody] ChangePasswordVM userCredentials)
        {
            var result = await accountRepository.ResetForgottenPassword(userCredentials);

            if (result == null)
                return NotFound();

            if (result.Succeeded)
                return Ok(result);
            return BadRequest(result.Errors);

        }

        [HttpGet("send-email")]
        public async Task<ActionResult> ResendEmailVerification([FromQuery] string email)
        {
            await accountRepository.ResendEmailVerification(email);
            return Ok();
        }


        [HttpGet("phone-verification")]
        public async Task<IActionResult> StartPhoneVerification([FromQuery] string id)
        {
            var result = accountRepository.StartPhoneVerification(id);

            if (result == null)
                return NotFound();

            if (result.Result.Errors != null)
                return BadRequest(result.Result.Errors);

            return Ok(result);
        }

        [HttpGet("check-phone-verification")]
        public async Task<IActionResult> StartPhoneVerification([FromQuery] string id, [FromQuery] string code)
        {
            var result = accountRepository.StartPhoneVerification(id, code);

            if (result == null)
                return NotFound();

            if (result.Result.Errors != null)
                return BadRequest(result.Result.Errors);
            else
                return Ok(result);
        }

        [HttpGet("changePass")]
        public async Task<ActionResult> ChangePassword(string id, [FromQuery] string newPass, [FromQuery] string oldPass)
        {
            var result = accountRepository.ChangePassword(id, newPass, oldPass);

            if (result.Result == null)
                return BadRequest("Old password is not correct!");

            if (result.Result.Succeeded)
                return Ok();
            else
                return BadRequest(result.Result.Errors);
        }

      

        [HttpPost("signIn")]
        public async Task<ActionResult<AuthentificationResponse>> SignIn([FromBody] LogInUserCredentials userCredentials)
        {
            var result = accountRepository.SignIn(userCredentials);

            if (result.Result == null)
                return BadRequest("Incorrect login");
            else
                return result.Result;
        }


        [HttpPost("signUp")]
        public async Task<ActionResult<AuthentificationResponse>> SignUp([FromBody] UserCredentials userCredentials)
        {
            var user = new UserAccount
            {
                UserName = userCredentials.UserName,
                Email = userCredentials.Email,
                FirstName = userCredentials.FirstName,
                LastName = userCredentials.LastName,
                PhoneNumber = userCredentials.PhoneNumber,
                Picture = "https://res.cloudinary.com/travelpal/image/upload/v1646346560/users/default_ordioo.jpg"
            };
            var result = await userManager.CreateAsync(user, userCredentials.Password);

            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

            var info = new Dictionary<string, string>
            {
                {"token", token },
                {"email", user.Email }
            };

            var callback = QueryHelpers.AddQueryString(clientURI, info);

            if (result.Succeeded) 
            {
                await accountRepository.SignUp(user, callback);
                return await tokenService.BuildToken(user);
            }

            else
                return BadRequest(result.Errors);
        }
    }
}

