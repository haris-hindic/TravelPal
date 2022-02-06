using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Helpers.Pagination;
using TravelPalAPI.Models;
using TravelPalAPI.ViewModels.Accommodation;

namespace TravelPalAPI.Repositories
{
    public interface IAccommodationRepository
    {
        Accommodation Add(AccommodationCreationVM accommodation);
        bool Update(int id, AccommodationEditVM accommodation);
        bool Ownership(string userId, int accommodationId);
        Task<PagedList<AccommodationVM>> GetAll(AccommodationSearchVM searchVM,UserParams userParams);
        AccommodationVM GetById(int id);
        Task<PagedList<AccommodationVM>> GetByUserId(string id,UserParams userParams);
        void Delete(int id);
        bool SaveChanges();
    }
}
