using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.ViewModels.Identity;
using TravelPalAPI.ViewModels.User;
using static TravelPalAPI.Controllers.UserController;

namespace TravelPalAPI.Repositories
{
    public interface IUserRepository
    {
        Task<UserProfileVM> UserProfile(string id);
        Task<bool> EditProfile(string id, EditProfileVM edit);
        Task<bool> Photo(string id, PictureVM formFile);
    }
}
