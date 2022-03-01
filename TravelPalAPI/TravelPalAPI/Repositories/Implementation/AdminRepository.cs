using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TravelPalAPI.Database;
using TravelPalAPI.Helpers.Pagination;
using TravelPalAPI.Models;
using TravelPalAPI.ViewModels.Identity;

namespace TravelPalAPI.Repositories.Implementation
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDbContext appDb;
        private readonly UserManager<UserAccount> userManager;
        private readonly string claimType = "role";
        private readonly string claimValue = "admin";

        public AdminRepository(AppDbContext appDb, UserManager<UserAccount> userManager)
        {
            this.appDb = appDb;
            this.userManager = userManager;
        }

        public async Task<IdentityResult> DeleteUser(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            return await userManager.DeleteAsync(user);
        }

        public async Task<PagedList<UserVM>> GetUsers(UserParams userParams)
        {
            var users = appDb.UserAccounts.OrderBy(x => x.UserName).Select(x =>
             new UserVM
             {
                 Id = x.Id,
                 UserName = x.UserName,
                 Picture = x.Picture
             });

            var usersVM = await PagedList<UserVM>.Create(users, userParams.PageNumber, userParams.PageSize);

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

        public async Task<IdentityResult> MakeAdmin(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            return await userManager.AddClaimAsync(user, new Claim("role", "admin"));
        }

        public async Task<IdentityResult> RemoveAdmin(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            return await userManager.RemoveClaimAsync(user, new Claim("role", "admin"));
        }
    }
}
