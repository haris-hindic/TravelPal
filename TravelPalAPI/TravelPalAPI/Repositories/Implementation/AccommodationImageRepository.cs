using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Database;
using TravelPalAPI.Helpers;
using TravelPalAPI.Models;
using TravelPalAPI.ViewModels.Accommodation;

namespace TravelPalAPI.Repositories.Implementation
{
    public class AccommodationImageRepository : IAccommodationImageRepository
    {
        private readonly AppDbContext appDb;
        private readonly string containerName = "Accommodation";
        private readonly IFileStorageService fileStorageService;

        public AccommodationImageRepository(AppDbContext appDb, IFileStorageService fileStorageService)
        {
            this.appDb = appDb;
            this.fileStorageService = fileStorageService;
        }
        public bool AddImages(int id, AccommodationImageCreationVM creationVM)
        {
            if (!appDb.Accommodations.Any(x => x.Id == id)) return false;

            foreach (var img in creationVM.Images)
            {
                appDb.AccommodationImages.Add(new AccommodationImage
                {
                    AccommodationId = id,
                    ImagePath = fileStorageService.SaveFile(containerName, img)
                });
            }

            return true;
        }

        public bool DeleteImage(int id)
        {
            var accImg = appDb.AccommodationImages.FirstOrDefault(x => x.Id == id);

            if (accImg == null) return false;

            fileStorageService.DeleteFile(accImg.ImagePath, containerName);

            appDb.AccommodationImages.Remove(accImg);
            return true;
        }

        public void SaveChanges()
        {
            appDb.SaveChanges();
        }
    }
}
