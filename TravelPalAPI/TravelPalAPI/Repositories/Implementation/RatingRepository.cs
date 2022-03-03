using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Database;
using TravelPalAPI.Models;
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

        public void Save()
        {
            appDb.SaveChanges();
        }
    }
}
