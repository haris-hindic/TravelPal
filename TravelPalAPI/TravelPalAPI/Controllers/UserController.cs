using AutoMapper;
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
    public class UserController : ControllerBase
    {
        private readonly UserManager<UserAccount> userManager;
        private readonly AppDbContext appDb;
        private readonly IFileStorageService storageService;
        private readonly IAccommodationRepository accRepo;
        private readonly IEventRepository eventRepo;
        private readonly IMapper imapper;

        public UserController(UserManager<UserAccount> userManager,AppDbContext appDb,IFileStorageService storageService,
            IAccommodationRepository accRepo,IEventRepository eventRepo,IMapper imapper)
        {
            this.userManager = userManager;
            this.appDb = appDb;
            this.storageService = storageService;
            this.accRepo = accRepo;
            this.eventRepo = eventRepo;
            this.imapper = imapper;
        }



        [HttpGet("profile")]
        public async Task<ActionResult<UserProfileVM>> UserProfile([FromQuery] string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null) return BadRequest("User not found!");

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
                Events= eventRepo.GetByUserId(user.Id)
            };
        }

        [HttpPut("edit-profile/{id}")]
        public async Task<IActionResult> EditProfile(string id, EditProfileVM edit)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null) return BadRequest("User not found!");

            user.UserName = edit.UserName;
            user.FirstName = edit.FirstName;
            user.LastName = edit.LastName;

            await userManager.UpdateAsync(user);

            return NoContent();
        }

        public class PictureVM
        {
            public IFormFile Picture { get; set; }
        }

        [HttpPost("change-photo/{id}")]
        public async Task<IActionResult> Photo(string id, [FromForm] PictureVM formFile)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null) return BadRequest("User not found!");

            if (!user.Picture.Contains("default"))
                storageService.DeleteFile(user.Picture, "User");

            user.Picture = storageService.SaveFile("User", formFile.Picture);

            await userManager.UpdateAsync(user);


            return Ok(appDb.SaveChanges());
        }
    }
}
