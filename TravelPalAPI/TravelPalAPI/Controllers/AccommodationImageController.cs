using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Database;
using TravelPalAPI.Helpers;
using TravelPalAPI.Models;
using TravelPalAPI.ViewModels.Accommodation;

namespace TravelPalAPI.Controllers
{
    [Route("api/image/accommodations")]
    [ApiController]
    public class AccommodationImageController : ControllerBase
    {
        private readonly AppDbContext appDb;
        private readonly IFileStorageService fileStorageService;
        private readonly string containerName = "Accommodation";

        public AccommodationImageController(AppDbContext appDb, IFileStorageService fileStorageService)
        {
            this.appDb = appDb;
            this.fileStorageService = fileStorageService;
        }

        [HttpPost, Route("{id}")]
        public IActionResult AddImages(int id, [FromForm] AccommodationImageCreationVM creationVM)
        {
            if (!appDb.Accommodations.Any(x => x.Id == id)) return NotFound();

            foreach (var img in creationVM.Images)
            {
                appDb.AccommodationImages.Add(new AccommodationImage
                {
                    AccommodationId = id,
                    Image = new Image
                    {
                        ImagePath = fileStorageService.SaveFile(containerName, img)
                    }
                });
            }

            appDb.SaveChanges();
            return Ok();
        }

        [HttpDelete, Route("{id}")]
        public IActionResult Delete(int id)
        {
            var accImg = appDb.AccommodationImages.FirstOrDefault(x => x.ImageId == id);
            var img = appDb.Images.FirstOrDefault(x => x.Id == id);

            if (img==null) return NotFound();

            fileStorageService.DeleteFile(img.ImagePath, containerName);

            appDb.AccommodationImages.Remove(accImg);
            appDb.Images.Remove(img);
            appDb.SaveChanges();
            return Ok();
        }
    }
}
