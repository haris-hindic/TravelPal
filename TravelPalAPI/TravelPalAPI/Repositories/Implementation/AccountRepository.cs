/*using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TravelPalAPI.Database;
using TravelPalAPI.Helpers;
using TravelPalAPI.Models;
using TravelPalAPI.Repositories.Implementation;
using TravelPalAPI.ViewModels.Identity;
using TravelPalAPI.ViewModels.User;

namespace TravelPalAPI.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext appDb;
        private readonly UserManager<UserAccount> userManager;
        private readonly SignInManager<UserAccount> signInManager;
        private readonly ITokenService tokenService;
        private readonly IEmailSenderService emailService;
        private readonly IPhoneVerificationService phoneVerification;
        private readonly string clientURI = "http://localhost:4200/authentication/emailconfirmation";
        private readonly string clientChangePassURI = "http://localhost:4200/input-new-password";
        private IConfiguration configuration;


        public AccountRepository(AppDbContext appDb, UserManager<UserAccount> userManager,
            SignInManager<UserAccount> signInManager, ITokenService tokenService,
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

        public ITokenService TokenService => tokenService;

        public async Task<AuthentificationResponse> SignIn(LogInUserCredentials userCredentials)
        {
            var user = await userManager.FindByNameAsync(userCredentials.UserName);


            var result = await signInManager.PasswordSignInAsync(userCredentials.UserName,
                userCredentials.Password, isPersistent: false, lockoutOnFailure: false);



            if (result.Succeeded)
            {
                return await tokenService.BuildToken(user);
            }
            else
                return null;
        }

        public async Task<AuthentificationResponse> SignUp(UserCredentials userCredentials)
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
                emailService.SendEmail(configuration, user.UserName, user.Email, "Confirmation",
                    $"Welcome {user.FirstName} {user.LastName}\nJust one more step before you get to the fun part.\n"
                   + $"Confirm your email address with this link:\n\n" +
                   $"{callback}");
                return await tokenService.BuildToken(user);
            }

            else
                return null;
        }

        public async Task<IdentityResult> EmailConfirmation(string email, string token)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return null;

            var confirmResult = await userManager.ConfirmEmailAsync(user, token);

            if (!confirmResult.Succeeded)
                return confirmResult;


            var userClaims = await userManager.GetClaimsAsync(user);

            if (!userClaims.Any(x => x.Type == "status" && x.Value == "verified"))
                await userManager.AddClaimAsync(user, new Claim("status", "verified"));

            return confirmResult;

        }

        public async Task SendForgotPassword(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return;

            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            var info = new Dictionary<string, string>
            {
                {"token", token },
                {"email", user.Email }
            };

            var callback = QueryHelpers.AddQueryString(clientChangePassURI, info);

            emailService.SendEmail(configuration, user.UserName, user.Email, "Reset password",
                    $"Dear {user.FirstName} {user.LastName}\nTo reset your password please click on link:\n\n" +
                   $"{callback}");
        }

        public async Task<IdentityResult> ResetForgottenPassword(ChangePasswordVM userCredentials)
        {

            var user = await userManager.FindByEmailAsync(userCredentials.Email);

            if (user == null)
                return null;

            var result = await userManager.ResetPasswordAsync(user, userCredentials.Token, userCredentials.Password);

            return result;

        }

        public async Task ResendEmailVerification(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return;

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

        }


        public async Task<VerificationResult> StartPhoneVerification(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null) return null;

            var result = await phoneVerification.StartVerificationAsync(user.PhoneNumber);

            if (result.Errors != null)
                return null;


            return result;
        }

        public async Task<VerificationResult> StartPhoneVerification(string id, string code)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null) return null;

            var result = await phoneVerification.CheckVerificationAsync(user.PhoneNumber, code);

            if (result.Errors != null)
                return null;

            user.PhoneNumberConfirmed = true;
            appDb.UserAccounts.Update(user);
            await appDb.SaveChangesAsync();

            var userClaims = await userManager.GetClaimsAsync(user);

            if (!userClaims.Any(x => x.Type == "status" && x.Value == "verified"))
                await userManager.AddClaimAsync(user, new Claim("status", "verified"));

            return result;
        }

        public async Task<IdentityResult> ChangePassword(string id, string newPass, string oldPass)
        {

            var user = appDb.UserAccounts.SingleOrDefault(x => x.Id == id);

            if (user == null)
                return null;


            var checkOldPassword = await signInManager.PasswordSignInAsync(user.UserName, oldPass, isPersistent: false, lockoutOnFailure: false);

            IdentityResult result = null;


            if (checkOldPassword.Succeeded)
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);

                result = await userManager.ResetPasswordAsync(user, token, newPass);

                if (!result.Succeeded)
                    return result;
            }
            else
            {
                return result;
            }

            return result;
         
        }

    }

}
*/
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TravelPalAPI.Database;
using TravelPalAPI.Helpers;
using TravelPalAPI.Models;
using TravelPalAPI.Repositories.Implementation;
using TravelPalAPI.ViewModels.Identity;
using TravelPalAPI.ViewModels.User;

namespace TravelPalAPI.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext appDb;
        private readonly UserManager<UserAccount> userManager;
        private readonly SignInManager<UserAccount> signInManager;
        private readonly ITokenService tokenService;
        private readonly IEmailSenderService emailService;
        private readonly IPhoneVerificationService phoneVerification;
        private readonly string clientURI = "http://localhost:4200/authentication/emailconfirmation";
        private readonly string clientChangePassURI = "http://localhost:4200/input-new-password";
        private IConfiguration configuration;


        public AccountRepository(AppDbContext appDb, UserManager<UserAccount> userManager,
            SignInManager<UserAccount> signInManager, ITokenService tokenService,
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

        public ITokenService TokenService => tokenService;

        public async Task<AuthentificationResponse> SignIn(LogInUserCredentials userCredentials)
        {
            var user = await userManager.FindByNameAsync(userCredentials.UserName);


            var result = await signInManager.PasswordSignInAsync(userCredentials.UserName,
                userCredentials.Password, isPersistent: false, lockoutOnFailure: false);



            if (result.Succeeded)
            {
                return await tokenService.BuildToken(user);
            }
            else
                return null;
        }

        public async Task SignUp(UserAccount user, string callback)
        {
            
                await emailService.SendEmail(configuration, user.UserName, user.Email, "Confirmation",
                $"Welcome {user.FirstName} {user.LastName}\nJust one more step before you get to the fun part.\n"
               + $"Confirm your email address with this link:\n\n" +
               $"{callback}");
        }


    public async Task<IdentityResult> EmailConfirmation(string email, string token)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return null;

            var confirmResult = await userManager.ConfirmEmailAsync(user, token);

            if (!confirmResult.Succeeded)
                return confirmResult;


            var userClaims = await userManager.GetClaimsAsync(user);

            if (!userClaims.Any(x => x.Type == "status" && x.Value == "verified"))
                await userManager.AddClaimAsync(user, new Claim("status", "verified"));

            return confirmResult;

        }

        public async Task SendForgotPassword(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return;

            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            var info = new Dictionary<string, string>
            {
                {"token", token },
                {"email", user.Email }
            };

            var callback = QueryHelpers.AddQueryString(clientChangePassURI, info);

            await emailService.SendEmail(configuration, user.UserName, user.Email, "Reset password",
                    $"Dear {user.FirstName} {user.LastName}\nTo reset your password please click on link:\n\n" +
                   $"{callback}");
        }

        public async Task<IdentityResult> ResetForgottenPassword(ChangePasswordVM userCredentials)
        {

            var user = await userManager.FindByEmailAsync(userCredentials.Email);

            if (user == null)
                return null;

            var result = await userManager.ResetPasswordAsync(user, userCredentials.Token, userCredentials.Password);

            return result;

        }

        public async Task ResendEmailVerification(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return;

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

        }


        public async Task<VerificationResult> StartPhoneVerification(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null) return null;

            var result = await phoneVerification.StartVerificationAsync(user.PhoneNumber);


            return result;
        }

        public async Task<VerificationResult> StartPhoneVerification(string id, string code)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null) return null;

            var result = await phoneVerification.CheckVerificationAsync(user.PhoneNumber, code);

            if (result.Errors != null)
                return null;

            user.PhoneNumberConfirmed = true;
            appDb.UserAccounts.Update(user);
            await appDb.SaveChangesAsync();

            var userClaims = await userManager.GetClaimsAsync(user);

            if (!userClaims.Any(x => x.Type == "status" && x.Value == "verified"))
                await userManager.AddClaimAsync(user, new Claim("status", "verified"));

            return result;
        }

        public async Task<IdentityResult> ChangePassword(string id, string newPass, string oldPass)
        {

            var user = appDb.UserAccounts.SingleOrDefault(x => x.Id == id);

            if (user == null)
                return null;


            var checkOldPassword = await signInManager.PasswordSignInAsync(user.UserName, oldPass, isPersistent: false, lockoutOnFailure: false);

            IdentityResult result = null;


            if (checkOldPassword.Succeeded)
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);

                result = await userManager.ResetPasswordAsync(user, token, newPass);

                if (!result.Succeeded)
                    return result;
            }

            return result;
         
        }

    }

}
