using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Database;
using TravelPalAPI.Models;
using TravelPalAPI.Repositories;
using TravelPalAPI.ViewModels.Rating;

namespace TravelPalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsVerified")]
    public class RatingsController : ControllerBase
    {
        private readonly IRatingRepository ratingRepository;
        private readonly IAccommodationRepository accommodationRepository;

        public RatingsController(IRatingRepository ratingRepository,IAccommodationRepository accommodationRepository)
        {
            this.ratingRepository = ratingRepository;
            this.accommodationRepository = accommodationRepository;
        }

        [HttpPost("rate")]
        public IActionResult RateAccommodation([FromBody] RatingCreationVM creationVM)
        {
            var ownerId = accommodationRepository.GetById(creationVM.AccommodationId).User.Id;

            if (ownerId == creationVM.UserId)
                return BadRequest();


            ratingRepository.RateAccommodation(creationVM);
            ratingRepository.Save();
            return Ok();
        }
    }
}
