using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Models;
using TravelPalAPI.ViewModels.Accommodation;

namespace TravelPalAPI.Repositories
{
    public interface IAccommodationRepository
    {
        Accommodation Add(AccommodationCreationVM accommodation);
        bool Update(int id, AccommodationEditVM accommodation);
        IEnumerable<AccommodationVM> GetAll();
        AccommodationVM GetById(int id);
        IEnumerable<AccommodationVM> GetByUserId(string id);
        void Delete(int id);
        bool SaveChanges();
    }
}
