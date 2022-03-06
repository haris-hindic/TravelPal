using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Helpers.Pagination;
using TravelPalAPI.ViewModels.Rating;

namespace TravelPalAPI.Repositories
{
    public interface IRatingRepository
    {
        void RateAccommodation(RatingCreationVM creationVM);
        Task<PagedList<RatingVM>> GetAll(UserParams userParams);
        int Delete(int id);
        void Save();
    }
}
