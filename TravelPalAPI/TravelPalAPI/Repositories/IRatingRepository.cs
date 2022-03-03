using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.ViewModels.Rating;

namespace TravelPalAPI.Repositories
{
    public interface IRatingRepository
    {
        void RateAccommodation(RatingCreationVM creationVM);
        void Save();
    }
}
