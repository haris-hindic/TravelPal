using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TravelPalAPI.Database;
using TravelPalAPI.Models;
using TravelPalAPI.ViewModels.Identity;

namespace TravelPalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext appDb;
        private readonly UserManager<UserAccount> userManager;
        private readonly IMapper mapper;
        private readonly string claimType = "role";
        private readonly string claimValue = "admin";

        public AdminController(AppDbContext appDb, UserManager<UserAccount> userManager, IMapper mapper)
        {
            this.appDb = appDb;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        //admin ctrl
        [HttpGet("users"), Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
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

        [HttpPost("makeAdmin"), 
            Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
        public async Task<ActionResult> MakeAdmin([FromBody] string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            await userManager.AddClaimAsync(user, new Claim("role", "admin"));
            return NoContent();
        }

        [HttpPost("removeAdmin"), 
            Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
        public async Task<ActionResult> RemoveAdmin([FromBody] string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            await userManager.RemoveClaimAsync(user, new Claim("role", "admin"));
            return NoContent();
        }

    }
}
