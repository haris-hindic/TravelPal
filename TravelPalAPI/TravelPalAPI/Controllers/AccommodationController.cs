using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Database;
using TravelPalAPI.Extensions;
using TravelPalAPI.Helpers;
using TravelPalAPI.Helpers.Pagination;
using TravelPalAPI.Models;
using TravelPalAPI.Repositories;
using TravelPalAPI.ViewModels.Accommodation;
using TravelPalAPI.ViewModels.AccommodationDetails;
using TravelPalAPI.ViewModels.AccommodationImage;
using TravelPalAPI.ViewModels.Identity;
using TravelPalAPI.ViewModels.Location;

namespace TravelPalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Policy = "IsVerified")]
    public class AccommodationController : ControllerBase
    {
        private readonly IAccommodationRepository accommodationRepo;


        public AccommodationController(IAccommodationRepository accommodationRepo)
        {
            this.accommodationRepo = accommodationRepo;
        }

        [HttpPost]
        public ActionResult<int> Post([FromBody] AccommodationCreationVM creationVM)
        {
            var obj = accommodationRepo.Add(creationVM);
            accommodationRepo.SaveChanges();
            return obj.Id;
        }

        [HttpPut,Route("{id}")]
        public IActionResult Update(int id, [FromBody] AccommodationEditVM accommodationEditVM)
        {
            var res = accommodationRepo.Update(id, accommodationEditVM);
            if (res == false) return NotFound();

            accommodationRepo.SaveChanges();
            return NoContent();
        }

        [HttpGet("ownership")]
        public ActionResult<bool> OwnerShip(string userId, int accommodationId)
        {
            return accommodationRepo.Ownership(userId, accommodationId);
        }

        [HttpGet,AllowAnonymous]
        public async Task<ActionResult<IEnumerable<AccommodationBasicVM>>> Get([FromQuery]string location, [FromQuery] double price,[FromQuery]UserParams userParams)
        {
            var searchVM = string.IsNullOrEmpty(location) && price==0 ? null : new AccommodationSearchVM { Location = location, Price = price };

            var accommodaions = await accommodationRepo.GetAll(searchVM, userParams);

            Response.AddPaginationHeader(accommodaions.CurrentPage, accommodaions.PageSize,
                accommodaions.TotalCount, accommodaions.TotalPages);
            
            return Ok(accommodaions);
        }

        [HttpGet, Route("user/{id}")]
        public async Task<ActionResult<PagedList<AccommodationBasicVM>>> GetByUser(string id, [FromQuery] UserParams userParams)
        {
            var accommodations = await accommodationRepo.GetByUserId(id,userParams);
            if (accommodations == null) return NotFound();

            Response.AddPaginationHeader(accommodations.CurrentPage, accommodations.PageSize,
                accommodations.TotalCount, accommodations.TotalPages);

            return Ok(accommodations);
        }


        [HttpGet,Route("{id}"), AllowAnonymous]
        public ActionResult<AccommodationVM> Get(int id)
        {
            var accommodations = accommodationRepo.GetById(id);
            if (accommodations == null) return BadRequest("Error!");

            return Ok(accommodations);
        }

        [HttpDelete,Route("{id}")]
        public IActionResult Delete(int id)
        {
            accommodationRepo.Delete(id);
            accommodationRepo.SaveChanges();
            return NoContent();
        }
    }
}
