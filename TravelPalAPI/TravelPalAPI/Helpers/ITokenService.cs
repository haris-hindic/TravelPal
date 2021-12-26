using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Models;
using TravelPalAPI.ViewModels.Identity;

namespace TravelPalAPI.Helpers
{
    public interface ITokenService
    {
        Task<AuthentificationResponse> BuildToken(UserAccount user);
    }
}
