using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TravelPalAPI.Database;
using TravelPalAPI.Extensions;
using TravelPalAPI.Helpers.Pagination;
using TravelPalAPI.Models;
using TravelPalAPI.Repositories;
using TravelPalAPI.ViewModels.Accommodation;
using TravelPalAPI.ViewModels.Event;
using TravelPalAPI.ViewModels.Identity;
using TravelPalAPI.ViewModels.Rating;

namespace TravelPalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository adminRepository;
        private readonly IAccommodationRepository accommodationRepository;
        private readonly IEventRepository eventRepository;
        private readonly IRatingRepository ratingRepository;

        public AdminController(IAdminRepository adminRepository,IAccommodationRepository accommodationRepository, 
            IEventRepository eventRepository, IRatingRepository ratingRepository)
        {
            this.adminRepository = adminRepository;
            this.accommodationRepository = accommodationRepository;
            this.eventRepository = eventRepository;
            this.ratingRepository = ratingRepository;
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

        [HttpGet("getStays")]
        public async Task<ActionResult<IEnumerable<AccommodationBasicVM>>> Get([FromQuery]UserParams userParams)
        {
            var accommodaions = await accommodationRepository.GetAll(null,userParams);

            Response.AddPaginationHeader(accommodaions.CurrentPage, accommodaions.PageSize,
                accommodaions.TotalCount, accommodaions.TotalPages);

            return Ok(accommodaions);
        }

        [HttpPost("deleteStay")]
        public ActionResult DeleteStay([FromBody] int id)
        {
            accommodationRepository.Delete(id);
            accommodationRepository.SaveChanges();
            return NoContent();
        }

        [HttpGet("getEvents")]
        public async Task<ActionResult<IEnumerable<EventVM>>> GetEvents([FromQuery] UserParams userParams)
        {
            var _events = await eventRepository.GetAll(userParams, null);
             
            Response.AddPaginationHeader(_events.CurrentPage, _events.PageSize,
                _events.TotalCount, _events.TotalPages);

            return Ok(_events);
        }

        [HttpPost("deleteEvent")]
        public ActionResult DeleteEvent([FromBody] int id)
        {
            eventRepository.Delete(id);
            return NoContent();
        }

        [HttpGet("getRatings")]
        public async Task<ActionResult<IEnumerable>> GetAllRatings([FromQuery] UserParams userParams)
        {
            var ratings = await ratingRepository.GetAll(userParams);

            Response.AddPaginationHeader(ratings.CurrentPage, ratings.PageSize,
                ratings.TotalCount, ratings.TotalPages);

            return Ok(ratings);
        }

        [HttpPost("deleteRating")]
        public ActionResult Delete([FromBody] int id)
        {
            var result = ratingRepository.Delete(id);

            if (result >= 0)
            {
                ratingRepository.Save();
                return Ok();
            }
            else
                return NotFound("Rating doesn't exists!");

           
        }
    }
}
