using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Database;
using TravelPalAPI.Helpers;
using TravelPalAPI.Models;
using TravelPalAPI.ViewModels.Event;

namespace TravelPalAPI.Repositories.Implementation
{
    public class EventImageRepository : IEventImageRepository
    {
        private readonly string containerName = "Event";
        private readonly IFileStorageService fileStorageService;
        AppDbContext _appDb;

        public EventImageRepository(AppDbContext dbContext, IFileStorageService storage)
        {
            this.fileStorageService = storage;
            this._appDb = dbContext;
        }

        [HttpPost]
        [Route("{id}")]
        public void Add(int id, [FromForm] EventImageCreationVM images)
        {
            if (!_appDb.Events.Any(e => e.Id == id))
            {
                return;
            }

            foreach (var image in images.Images)
            {
                _appDb.EventImages.Add(new EventImages
                {
                    EventId = id,
                    ImagePath = fileStorageService.SaveFile(containerName, image)
                }
            );
            }
            _appDb.SaveChanges();
        }

        [HttpDelete]
        [Route("{id}")]
        public void Delete(int id)
        {
            var img = _appDb.EventImages.SingleOrDefault(e => e.Id == id);

            fileStorageService.DeleteFile(img.ImagePath, containerName);
            _appDb.EventImages.Remove(img);

            _appDb.SaveChanges();
        }
    }
}
