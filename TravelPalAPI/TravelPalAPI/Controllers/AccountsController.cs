﻿using AutoMapper;
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
using TravelPalAPI.ViewModels.User;

namespace TravelPalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly AppDbContext appDb;
        private readonly UserManager<UserAccount> userManager;
        private readonly SignInManager<UserAccount> signInManager;
        private readonly ITokenService tokenService;
        private readonly IEmailSenderService emailService;
        private readonly IPhoneVerificationService phoneVerification;
        private readonly string clientURI= "http://localhost:4200/authentication/emailconfirmation";
        private IConfiguration configuration;


        public AccountsController(AppDbContext appDb,UserManager<UserAccount> userManager,
            SignInManager<UserAccount> signInManager,ITokenService tokenService,
            IConfiguration configuration, IEmailSenderService emailService,
            IPhoneVerificationService phoneVerification)
        {
            this.appDb = appDb;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenService = tokenService;
            this.configuration = configuration;
            this.emailService = emailService;
            this.phoneVerification = phoneVerification;
        }

        
        [HttpPost("signIn")]
        public async Task<ActionResult<AuthentificationResponse>> SignIn([FromBody] LogInUserCredentials userCredentials)
        {
            var user = await userManager.FindByNameAsync(userCredentials.UserName);


            var result = await signInManager.PasswordSignInAsync(userCredentials.UserName, 
                userCredentials.Password, isPersistent: false, lockoutOnFailure: false);



            if (result.Succeeded)
            {
                return await tokenService.BuildToken(user);
            }
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
                PhoneNumber = userCredentials.PhoneNumber,
                Picture= "https://localhost:44325/User/default.jpg"
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
                emailService.SendEmail(configuration, user.UserName, user.Email, "Confirmation",
                    $"Welcome {user.FirstName} {user.LastName}\nJust one more step before you get to the fun part.\n"
                   +$"Confirm your email address with this link:\n\n" +
                   $"{callback}");
                return await tokenService.BuildToken(user);
            }

            else
                return BadRequest(result.Errors);
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


            var userClaims = await userManager.GetClaimsAsync(user);

            if(!userClaims.Any(x=>x.Type=="status" && x.Value == "verified"))
                await userManager.AddClaimAsync(user, new Claim("status", "verified"));

            return Ok();
        }

        [HttpGet("send-email")]
        public async Task<ActionResult> ResendEmailVerification([FromQuery] string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest();

            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

            var info = new Dictionary<string, string>
            {
                {"token", token },
                {"email", user.Email }
            };

            var callback = QueryHelpers.AddQueryString(clientURI, info);

            emailService.SendEmail(configuration, user.UserName, user.Email, "Confirmation",
                    $"Welcome {user.FirstName} {user.LastName}\nJust one more step before you get to the fun part.\n"
                   + $"Confirm your email address with this link:\n\n" +
                   $"{callback}");

            return Ok();
        }


        [HttpGet("phone-verification")]
        public async Task<IActionResult> StartPhoneVerification([FromQuery] string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null) return BadRequest("User not found!");

            var result = await phoneVerification.StartVerificationAsync(user.PhoneNumber);

            if (result.Errors != null)
                return BadRequest(result.Errors);


            return Ok(result);
        }

        [HttpGet("check-phone-verification")]
        public async Task<IActionResult> StartPhoneVerification([FromQuery] string id,[FromQuery]string code)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null) return BadRequest("User not found!");

            var result = await phoneVerification.CheckVerificationAsync(user.PhoneNumber,code);

            if (result.Errors != null)
                return BadRequest(result.Errors);

            user.PhoneNumberConfirmed = true;
            appDb.UserAccounts.Update(user);
            await appDb.SaveChangesAsync();

            var userClaims = await userManager.GetClaimsAsync(user);

            if (!userClaims.Any(x => x.Type == "status" && x.Value == "verified"))
                await userManager.AddClaimAsync(user, new Claim("status", "verified"));

            return Ok(result);
        }

    }
}
