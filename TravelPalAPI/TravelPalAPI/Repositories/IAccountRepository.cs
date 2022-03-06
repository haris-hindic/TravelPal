using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Helpers;
using TravelPalAPI.Models;
using TravelPalAPI.ViewModels.Identity;
using TravelPalAPI.ViewModels.User;

namespace TravelPalAPI.Repositories.Implementation
{
    public interface IAccountRepository
    {
        Task<AuthentificationResponse> SignIn(LogInUserCredentials userCredentials);
        Task SignUp(UserAccount user, string callback);

        Task<IdentityResult> EmailConfirmation(string email, string token);
        Task SendForgotPassword(string email);
        Task<IdentityResult> ResetForgottenPassword(ChangePasswordVM userCredentials);
        Task ResendEmailVerification(string email);
        Task<VerificationResult> StartPhoneVerification(string id);
        Task<VerificationResult> StartPhoneVerification(string id,  string code);
        Task<IdentityResult> ChangePassword(string id, string newPass, string oldPass);

    }
}
