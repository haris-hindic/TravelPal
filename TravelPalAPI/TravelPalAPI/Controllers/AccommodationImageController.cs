using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Database;
using TravelPalAPI.Helpers;
using TravelPalAPI.Models;
using TravelPalAPI.Repositories;
using TravelPalAPI.ViewModels.Accommodation;

namespace TravelPalAPI.Controllers
{
    [Route("api/image/accommodations")]
    [ApiController]
    public class AccommodationImageController : ControllerBase
    {
        private readonly IAccommodationImageRepository imageRepo;

        public AccommodationImageController(IAccommodationImageRepository imageRepo)
        {
            this.imageRepo = imageRepo;
        }

        [HttpPost, Route("{id}")]
        public IActionResult AddImages(int id, [FromForm] AccommodationImageCreationVM creationVM)
        {
            var result = imageRepo.AddImages(id, creationVM);
            if (result == false) return NotFound("No such accommodation!");

            imageRepo.SaveChanges();
            return Ok();
        }

        [HttpDelete, Route("{id}")]
        public IActionResult Delete(int id)
        {
            var result = imageRepo.DeleteImage(id);
            if (result == false) return NotFound("No such image!");

            imageRepo.SaveChanges();
            return Ok();
        }
    }
}
