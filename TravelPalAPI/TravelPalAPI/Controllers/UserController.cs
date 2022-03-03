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
using System.Threading.Tasks;
using TravelPalAPI.Database;
using TravelPalAPI.Helpers;
using TravelPalAPI.Helpers.Pagination;
using TravelPalAPI.Models;
using TravelPalAPI.Repositories;
using TravelPalAPI.ViewModels.Accommodation;
using TravelPalAPI.ViewModels.Identity;
using TravelPalAPI.ViewModels.User;

namespace TravelPalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet("profile")]
        public async Task<ActionResult<UserProfileVM>> UserProfile([FromQuery] string id)
        {
            var profile = await userRepository.UserProfile(id);
            return profile != null ? profile : BadRequest("User not found!");
        }

        [HttpPut("edit-profile/{id}")]
        public async Task<IActionResult> EditProfile(string id, EditProfileVM edit)
        {
            var result = await userRepository.EditProfile(id, edit);

            return result != false ? Ok() : BadRequest("Error!"); 
        }

        public class PictureVM
        {
            public IFormFile Picture { get; set; }
        }

        [HttpPost("change-photo/{id}")]
        public async Task<IActionResult> Photo(string id, [FromForm] PictureVM formFile)
        {
            var result = await userRepository.Photo(id, formFile);

            return result != false ? Ok() : BadRequest("Error!");
        }
    }
}
