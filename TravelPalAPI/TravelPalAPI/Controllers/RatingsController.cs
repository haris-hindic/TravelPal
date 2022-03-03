using AutoMapper;
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
    public class RatingsController : ControllerBase
    {
        private readonly IRatingRepository ratingRepository;

        public RatingsController(IRatingRepository ratingRepository)
        {
            this.ratingRepository = ratingRepository;
        }

        [HttpPost("rate")]
        public IActionResult RateAccommodation([FromBody] RatingCreationVM creationVM)
        {
            ratingRepository.RateAccommodation(creationVM);
            ratingRepository.Save();
            return Ok();
        }
    }
}
