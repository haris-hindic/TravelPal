using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Database;
using TravelPalAPI.Helpers;
using TravelPalAPI.Models;
using TravelPalAPI.ViewModels.Accommodation;
using TravelPalAPI.ViewModels.AccommodationDetails;
using TravelPalAPI.ViewModels.Location;

namespace TravelPalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccommodationController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly AppDbContext appDb;

        public AccommodationController(AppDbContext appDb,IMapper mapper)
        {
            this.appDb = appDb;
            this.mapper = mapper;
        }

        [HttpPost]
        public IActionResult Post([FromBody] AccommodationCreationVM creationVM)
        {
            var obj = mapper.Map<Accommodation>(creationVM);

            appDb.Accommodations.Add(obj);
            appDb.SaveChanges();
            return Ok("Succesfully created!");
        }

        [HttpPut,Route("{id}")]
        public IActionResult Update(int id, [FromBody] AccommodationEditVM accommodationEditVM)
        {
            var x =appDb.Accommodations.FirstOrDefault(x => x.Id == id);
            
            if (x==null) NotFound();


            x.Name = accommodationEditVM.Name;
            x.Price = accommodationEditVM.Price;
            x.Location = mapper.Map<Location>(accommodationEditVM.Location);
            x.AccommodationDetails = mapper.Map<AccommodationDetails>(accommodationEditVM.AccommodationDetails);

            appDb.Update(x);
            appDb.SaveChanges();
            return Ok("Updated succesfully!");
        }

        [HttpGet]
        public ActionResult<IEnumerable<AccommodationVM>> Get()
        {
            var accommodations = appDb.Accommodations
               .Select(x => new AccommodationVM
               {
                   Id = x.Id,
                   Name = x.Name,
                   Price = x.Price,
                   Location = mapper.Map<LocationVM>(x.Location),
                   AccommodationDetails = mapper.Map<AccommodationDetailsVM>(x.AccommodationDetails),
                   Images = x.AccommodationImages.Select(x => x.Image).ToList()

               }).ToList();
            return accommodations;
        }


        [HttpGet,Route("{id}")]
        public ActionResult<AccommodationVM> Get(int id)
        {
            if (appDb.Accommodations.Any(x => x.Id == id))
                NotFound();

            var accommodation = appDb.Accommodations
                .Select(x => new AccommodationVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Location = mapper.Map<LocationVM>(x.Location),
                    AccommodationDetails = mapper.Map<AccommodationDetailsVM>(x.AccommodationDetails),
                    Images = x.AccommodationImages.Select(x => x.Image).ToList()
                }).FirstOrDefault(x => x.Id == id);

            return accommodation;
        }

        [HttpDelete,Route("{id}")]
        public IActionResult Delete(int id)
        {
            var accommodation = appDb.Accommodations.FirstOrDefault(x => x.Id == id);
            var accommodationDetails = appDb.AccommodationDetails.FirstOrDefault(x => x.Id == accommodation.AccommodationDetailsId);
            IEnumerable<AccommodationImage> accommodationImages = appDb.AccommodationImages.Where(x => x.AccommodationId == accommodation.Id).ToList();
            var accommodationLocation = appDb.Locations.FirstOrDefault(x => x.Id== accommodation.LocationId);

            appDb.Accommodations.Remove(accommodation);
            appDb.AccommodationDetails.Remove(accommodationDetails);
            appDb.AccommodationImages.RemoveRange(accommodationImages);
            appDb.Locations.Remove(accommodationLocation);
            appDb.SaveChanges();
            return Ok("Succesfully deleted!");
        }
    }
}
