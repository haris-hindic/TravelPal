using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Helpers.Pagination;
using TravelPalAPI.ViewModels.Accommodation;
using TravelPalAPI.ViewModels.Identity;

namespace TravelPalAPI.Repositories
{
    public interface IAdminRepository
    {
        Task<PagedList<UserVM>> GetUsers(UserParams userParams);
        Task<IdentityResult> MakeAdmin(string userId);
        Task<IdentityResult> RemoveAdmin(string userId);
        Task<IdentityResult> DeleteUser(string userId);
    }
}
