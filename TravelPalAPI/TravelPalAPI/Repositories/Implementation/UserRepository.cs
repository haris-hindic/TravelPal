using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Controllers;
using TravelPalAPI.Database;
using TravelPalAPI.Helpers;
using TravelPalAPI.Helpers.Pagination;
using TravelPalAPI.Models;
using TravelPalAPI.ViewModels.Identity;
using TravelPalAPI.ViewModels.User;

namespace TravelPalAPI.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<UserAccount> userManager;
        private readonly AppDbContext appDb;
        private readonly IFileStorageService storageService;
        private readonly IAccommodationRepository accRepo;
        private readonly IEventRepository eventRepo;
        private readonly IMessageRepository messageRepo;

        public UserRepository(UserManager<UserAccount> userManager, AppDbContext appDb,  IFileStorageService storageService,
            IAccommodationRepository accRepo, IEventRepository eventRepo, IMessageRepository messageRepo)
        {
            this.userManager = userManager;
            this.appDb = appDb;
            this.storageService = storageService;
            this.accRepo = accRepo;
            this.eventRepo = eventRepo;
            this.messageRepo = messageRepo;
        }

        public async Task<bool> EditProfile(string id, EditProfileVM edit)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null) return false;

            user.UserName = edit.UserName;
            user.FirstName = edit.FirstName;
            user.LastName = edit.LastName;

            await userManager.UpdateAsync(user);
            return true;
        }

        public async Task<bool> Photo(string id, UserController.PictureVM formFile)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null) return false;

            if (!user.Picture.Contains("default"))
                storageService.DeleteFile(user.Picture, "User");

            user.Picture = storageService.SaveFile("User", formFile.Picture);

            await userManager.UpdateAsync(user);


            return true;
        }

        public async Task<UserProfileVM> UserProfile(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null) return null;

            return new UserProfileVM
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                EmailVerified = user.EmailConfirmed,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberVerified = user.PhoneNumberConfirmed,
                Picture = user.Picture,
                Accommodations = accRepo.GetByUserId(id, new UserParams()).Result,
                Events = eventRepo.GetByUserId(user.Id, new UserParams()).Result,
                MessagesReceived = messageRepo.GetReceivedMsg(user.Id)
            };
        }
    }
}
