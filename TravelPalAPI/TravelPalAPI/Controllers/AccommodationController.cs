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
using TravelPalAPI.ViewModels.Location;

namespace TravelPalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccommodationController : ControllerBase
    {
        private readonly AppDbContext appDb;
        private readonly IFileStorageService fileStorageService;
        private readonly string containerName="Accommodation";

        public AccommodationController(AppDbContext appDb,IFileStorageService fileStorageService)
        {
            this.appDb = appDb;
            this.fileStorageService = fileStorageService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] AccommodationCreationVM creationVM)
        {
            var obj = new Accommodation
            {
                Name = creationVM.Name,
                Price = creationVM.Price,
                Location = new Location
                {
                    Country = creationVM.LocationCreation.Country,
                    City = creationVM.LocationCreation.City,
                    Address = creationVM.LocationCreation.Address
                },
                AccommodationDetails = new AccommodationDetails
                {
                    Parking = creationVM.AccommodationDetailsCreation.Parking,
                    Minibar= creationVM.AccommodationDetailsCreation.Minibar,
                    AirConditioning= creationVM.AccommodationDetailsCreation.AirConditioning,
                    Safe= creationVM.AccommodationDetailsCreation.Safe,
                    Dryer= creationVM.AccommodationDetailsCreation.Dryer,
                    FlatScreenTV= creationVM.AccommodationDetailsCreation.FlatScreenTV,
                    BBQ= creationVM.AccommodationDetailsCreation.BBQ,
                    Refrigerator= creationVM.AccommodationDetailsCreation.Refrigerator,
                    Balcony= creationVM.AccommodationDetailsCreation.Balcony,
                    MosquitoNet= creationVM.AccommodationDetailsCreation.MosquitoNet
                }
            };

            appDb.Accommodations.Add(obj);
            appDb.SaveChanges();
            return Ok("Succesfully created!");
        }

        [HttpPost,Route("add-images/{id}")]
        public IActionResult AddImages(int id, [FromForm] AccommodationImageCreationVM creationVM)
        {
            var accommodation = appDb.Accommodations.FirstOrDefault(x => x.Id == id);

            foreach (var img in creationVM.Images)
            {
                appDb.AccommodationImages.Add(new AccommodationImage 
                { 
                    AccommodationId = accommodation.Id, Image = new Image 
                    {
                        ImagePath = fileStorageService.SaveFile(containerName,img) 
                    } 
                });
            }

            appDb.SaveChanges();
            return Ok();
        }

        [HttpPut,Route("update/{id}")]
        public IActionResult Update(int id, [FromBody] AccommodationEditVM accommodationEditVM)
        {
            var x =appDb.Accommodations.FirstOrDefault(x => x.Id == id);
            
            if (x==null)
                NotFound();

            x.Name = accommodationEditVM.Name;
            x.Price = accommodationEditVM.Price;
            x.Location = accommodationEditVM.Location;
            x.AccommodationDetails = accommodationEditVM.AccommodationDetails;


            appDb.Update(x);
            appDb.SaveChanges();
            return Ok("Updated succesfully!");
        }

        [HttpGet("getall")]
        public ActionResult<IEnumerable<AccommodationVM>> Get()
        {
            var accommodations = appDb.Accommodations
                .Select(x => new AccommodationVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Location = x.Location,
                    AccommodationDetails = x.AccommodationDetails,
                    Images = x.AccommodationImages.Select(x => x.Image).ToList()
                }).ToList();

            return accommodations;
        }


        [HttpGet,Route("get/{id}")]
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
                    Location = x.Location,
                    AccommodationDetails = x.AccommodationDetails,
                    Images = x.AccommodationImages.Select(x => x.Image).ToList()
                }).FirstOrDefault(x => x.Id == id);

            return accommodation;
        }

        [HttpDelete,Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var accommodation = appDb.Accommodations.FirstOrDefault(x => x.Id == id);
            var accommodationDetails = appDb.AccommodationDetails.FirstOrDefault(x => x.Id == accommodation.AccommodationDetailsId);
            IEnumerable<AccommodationImage> accommodationImages = appDb.AccommodationImages.Where(x => x.AccommodationId == accommodation.Id);
            var accommodationLocation = appDb.Locations.FirstOrDefault(x => x.Id == accommodation.LocationId);

            appDb.Accommodations.Remove(accommodation);
            appDb.AccommodationDetails.Remove(accommodationDetails);
            appDb.AccommodationImages.RemoveRange(accommodationImages);
            appDb.Locations.Remove(accommodationLocation);
            appDb.SaveChanges();
            return Ok("Succesfully deleted!");
        }
    }
}
