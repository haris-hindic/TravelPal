using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Database;
using TravelPalAPI.Helpers;
using TravelPalAPI.Models;
using TravelPalAPI.ViewModels.Event;

namespace TravelPalAPI.Controllers
{

   
    [Route("api/image/events")]
    [ApiController]

    public class EventImageController : ControllerBase
    {
        private readonly string containerName = "Event";
        private readonly IFileStorageService fileStorageService;
        AppDbContext _appDb;

        public EventImageController(AppDbContext dbContext, IFileStorageService storage)
        {
            this.fileStorageService = storage;
            this._appDb = dbContext;
        }

        [HttpPost]
        [Route("{id}")]
        public IActionResult Add(int id, [FromForm] EventImageCreationVM images)
        {
           if(!_appDb.Events.Any(e=> e.Id == id))
           {
                return NotFound();
           }

            foreach (var image in images.Images)
            {
                _appDb.EventImages.Add(new EventImages
                {
                    EventId = id,
                    Image = new Image(){ ImagePath = fileStorageService.SaveFile(containerName, image) }
                }
            );
            }
            _appDb.SaveChanges();
             return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            if(!_appDb.Images.Any(e=> e.Id == id) || !_appDb.EventImages.Any(e=> e.ImageId==id))
            {
                return NotFound();
            }

            var img = _appDb.Images.SingleOrDefault(e => e.Id == id);
            var imgEvent = _appDb.EventImages.SingleOrDefault(e => e.ImageId == id);

            fileStorageService.DeleteFile(img.ImagePath, containerName);
            _appDb.Images.Remove(img);
            _appDb.EventImages.Remove(imgEvent);

            _appDb.SaveChanges();
            return Ok();
        }
    }
}
