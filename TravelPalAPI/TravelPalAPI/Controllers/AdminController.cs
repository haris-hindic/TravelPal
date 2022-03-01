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
using TravelPalAPI.Extensions;
using TravelPalAPI.Helpers.Pagination;
using TravelPalAPI.Models;
using TravelPalAPI.Repositories;
using TravelPalAPI.ViewModels.Identity;

namespace TravelPalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository adminRepository;

        public AdminController(IAdminRepository adminRepository)
        {
            this.adminRepository = adminRepository;
        }

        [HttpGet("users")]
        public async Task<ActionResult<PagedList<UserVM>>> GetUsers([FromQuery] UserParams userParams)
        {
            var usersVM = await adminRepository.GetUsers(userParams);

            Response.AddPaginationHeader(usersVM.CurrentPage, usersVM.PageSize, usersVM.TotalCount, usersVM.TotalPages);

            return usersVM;
        }

        [HttpPost("makeAdmin")]
        public async Task<ActionResult> MakeAdmin([FromBody] string userId)
        {
            await adminRepository.MakeAdmin(userId);
            return NoContent();
        }

        [HttpPost("removeAdmin")]
        public async Task<ActionResult> RemoveAdmin([FromBody] string userId)
        {
            await adminRepository.RemoveAdmin(userId);
            return NoContent();
        }

        [HttpPost("deleteUser")]
        public async Task<ActionResult> DeleteUser([FromBody] string userId)
        {
            await adminRepository.DeleteUser(userId);
            return NoContent();
        }

    }
}
