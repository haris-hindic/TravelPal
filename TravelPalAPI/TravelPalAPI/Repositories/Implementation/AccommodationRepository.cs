using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Database;
using TravelPalAPI.Helpers;
using TravelPalAPI.Helpers.Pagination;
using TravelPalAPI.Models;
using TravelPalAPI.ViewModels.Accommodation;
using TravelPalAPI.ViewModels.AccommodationDetails;
using TravelPalAPI.ViewModels.AccommodationImage;
using TravelPalAPI.ViewModels.Identity;
using TravelPalAPI.ViewModels.Location;
using TravelPalAPI.ViewModels.Reservation;

namespace TravelPalAPI.Repositories.Implementation
{
    public class AccommodationRepository : IAccommodationRepository
    {
        private readonly AppDbContext appDb;
        private readonly IMapper mapper;
        private readonly IFileStorageService imageService;
        private readonly string containerName = "Accommodation";

        public AccommodationRepository(AppDbContext appDb, IMapper mapper, IFileStorageService imageService)
        {
            this.appDb = appDb;
            this.mapper = mapper;
            this.imageService = imageService;
        }

        public Accommodation Add(AccommodationCreationVM accommodation)
        {
            var obj = mapper.Map<Accommodation>(accommodation);


            appDb.Accommodations.Add(obj);
            return obj;
        }

        public bool Update(int id, AccommodationEditVM accommodation)
        {
            var acc = appDb.Accommodations.FirstOrDefault(x => x.Id == id);

            if (acc == null) return false;


            acc.Name = accommodation.Name;
            acc.Price = accommodation.Price;
            acc.Rooms = accommodation.Rooms;
            acc.Description = accommodation.Description;
            acc.Capacity = accommodation.Capacity;
            acc.Location = mapper.Map<Location>(accommodation.Location);
            var accommodationDetails = mapper.Map<AccommodationDetails>(accommodation.AccommodationDetails);

            appDb.Update(accommodationDetails);
            appDb.Update(acc);
            return true;
        }

        public void Delete(int id)
        {
            var accommodation = appDb.Accommodations.FirstOrDefault(x => x.Id == id);
            var accommodationLocation = appDb.Locations.FirstOrDefault(x => x.Id == accommodation.LocationId);
            var images = appDb.AccommodationImages.Where(x => x.AccommodationId == id).ToList();

            foreach (var img in images)
            {
                imageService.DeleteFile(img.ImagePath, containerName);
            }

            appDb.Accommodations.Remove(accommodation);
            appDb.Locations.Remove(accommodationLocation);
        }

        public async Task<PagedList<AccommodationBasicVM>> GetAll(AccommodationSearchVM searchVM,UserParams userParams)
        {
            IQueryable<AccommodationBasicVM> accommodations;

            if (searchVM != null)
            {

                accommodations = appDb.Accommodations
               .Where(x => ((string.IsNullOrEmpty(searchVM.Location) ||
                            x.Location.Address.ToLower().StartsWith(searchVM.Location.ToLower()) ||
                            x.Location.City.Name.ToLower().StartsWith(searchVM.Location.ToLower()) ||
                            x.Location.City.Country.Name.ToLower().StartsWith(searchVM.Location.ToLower()))
                            && (searchVM.Price == 0 || x.Price <= searchVM.Price)))
              .Select(x => new AccommodationBasicVM
              {
                  Id = x.Id,
                  Name = x.Name,
                  Price = x.Price,
                  Image = x.AccommodationImages.First().ImagePath,
                  Country = x.Location.City.Country.Name,
                  Address = x.Location.Address,
                  City = x.Location.City.Name,
                  Latitude = x.Location.Latitude,
                  Longitude = x.Location.Longitude,
                  UserImage = x.Host.Picture

              }).OrderBy(x => x.Name).AsNoTracking();

            }
            else
            {
                accommodations = appDb.Accommodations
               .Select(x => new AccommodationBasicVM
               {
                   Id = x.Id,
                   Name = x.Name,
                   Price = x.Price,
                   Image = x.AccommodationImages.First().ImagePath,
                   Country = x.Location.City.Country.Name,
                   Address=x.Location.Address,
                   City = x.Location.City.Name,
                   Latitude = x.Location.Latitude,
                   Longitude = x.Location.Longitude,
                  UserImage = x.Host.Picture

               }).OrderBy(x=>x.Name).AsNoTracking();
            }

            var x = accommodations.ToList();

            return await PagedList<AccommodationBasicVM>.Create(accommodations, userParams.PageNumber, userParams.PageSize);
        }


        public AccommodationVM GetById(int id)
        {
            if (!appDb.Accommodations.Any(x => x.Id == id))
                return null;

            var accommodation = appDb.Accommodations.Include(x => x.Location).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                .Select(x => new AccommodationVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Description = x.Description,
                    Capacity = x.Capacity,
                    Rooms=x.Rooms,
                    User = mapper.Map<UserVM>(x.Host),
                    Location = mapper.Map<LocationVM>(x.Location),
                    AccommodationDetails = mapper.Map<AccommodationDetailsVM>(x.AccommodationDetails),
                    Images = mapper.Map<List<AccommodationImageVM>>(x.AccommodationImages),
                    DateReserved= x.Reservations.Where(x=>x.Status.Description=="Active").Select(x=>new ReservationDatesVM { End=x.End,Start=x.Start}).ToList()
                }).FirstOrDefault(x => x.Id == id);

            return accommodation;
        }

        public async Task<PagedList<AccommodationBasicVM>> GetByUserId(string id, UserParams userParams)
        {
            if (!appDb.UserAccounts.Any(x => x.Id == id)) return null;

            var accommodations = appDb.Accommodations.Where(x => x.HostId == id)
                .Include(x => x.Location).ThenInclude(x => x.City).ThenInclude(x => x.Country)
               .Select(x => new AccommodationBasicVM
               {
                   Id = x.Id,
                   Name = x.Name,
                   Price = x.Price,
                   Image = x.AccommodationImages.First().ImagePath,
                   Country = x.Location.City.Country.Name,
                   City = x.Location.City.Name,
                   Address = x.Location.Address,
                   Latitude = x.Location.Latitude,
                   Longitude = x.Location.Longitude,
                   UserImage = x.Host.Picture

               });

            return await PagedList<AccommodationBasicVM>.Create(accommodations,userParams.PageNumber,userParams.PageSize);
        }

        public bool SaveChanges()
        {
            return appDb.SaveChanges() > 0;
        }

        public bool Ownership(string userId, int accommodationId)
        {
            var accommodation = appDb.Accommodations.FirstOrDefault(x => x.Id == accommodationId);

            
            if (accommodation!=null && accommodation.HostId == userId)
                return true;

            return false;
        }
    }
}
