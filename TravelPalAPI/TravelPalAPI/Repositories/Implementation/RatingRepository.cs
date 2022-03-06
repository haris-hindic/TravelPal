using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Database;
using TravelPalAPI.Helpers.Pagination;
using TravelPalAPI.Models;
using TravelPalAPI.ViewModels.Accommodation;
using TravelPalAPI.ViewModels.Identity;
using TravelPalAPI.ViewModels.Rating;

namespace TravelPalAPI.Repositories.Implementation
{
    public class RatingRepository : IRatingRepository
    {
        private readonly AppDbContext appDb;
        private readonly IMapper imapper;

        public RatingRepository(AppDbContext appDb, IMapper imapper)
        {
            this.appDb = appDb;
            this.imapper = imapper;
        }

        public void RateAccommodation(RatingCreationVM creationVM)
        {
            var obj = imapper.Map<Rating>(creationVM);

            appDb.Ratings.Add(obj);
        }

        public async Task<PagedList<RatingVM>> GetAll(UserParams _params)
        {
            var obj = appDb.Ratings.Select(x => new RatingVM()
            {
                Rate = x.Rate,
                Comment = x.Comment,
                User = x.User.UserName,
                Accommodation = imapper.Map<AccommodationVM>(x.Accommodation),
                Id = x.Id
            });

            if (obj == null)
                return null;

            return await PagedList<RatingVM>.Create(obj, _params.PageNumber, _params.PageSize);

        }

        public int Delete(int id)
        {
            var rating = appDb.Ratings.SingleOrDefault(x => x.Id == id);

            if (rating == null)
                return -1;

            appDb.Ratings.Remove(rating);

            return rating.Id;
        }


        public void Save()
        {
            appDb.SaveChanges();
        }
    }
}
