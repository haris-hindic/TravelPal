using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.ViewModels.Accommodation;

namespace TravelPalAPI.Repositories
{
    public interface IAccommodationImageRepository
    {
        bool AddImages(int id, AccommodationImageCreationVM creationVM);
        bool DeleteImage(int id);
        void SaveChanges();
    }
}
