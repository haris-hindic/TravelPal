using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Database;
using TravelPalAPI.Helpers;
using TravelPalAPI.Models;
using TravelPalAPI.ViewModels.Event;
using TravelPalAPI.ViewModels.EventImages;
using TravelPalAPI.ViewModels.Identity;
using TravelPalAPI.ViewModels.Location;

namespace TravelPalAPI.Repositories.Implementation
{
    public class EventRepository :  IEventRepository
    {
        private readonly AppDbContext appDb;
        private readonly IMapper mapper;

        public EventRepository(AppDbContext appDb, IMapper mapper, IFileStorageService imgService)
        {
            this.appDb = appDb;
            this.mapper = mapper;
        }

        [HttpPost]
        public int Post([FromBody] EventCreationVM eventCreation)
        {
            var _event = mapper.Map<Event>(eventCreation);

            appDb.Events.Add(_event);

            appDb.SaveChanges();

            return _event.Id;
        }

        [HttpGet]
        [Route("{id}")]
        public EventVM Get(int id)
        {
            if (!appDb.Events.Any(e => e.Id == id))
                return null;

            return appDb.Events.Include(x => x.Location).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                .Select(e => new EventVM
                {
                    Id = e.Id,
                    Name = e.Name,
                    Date = e.Date,
                    Price = e.Price,
                    EventDescription = e.EventDescription,
                    User = mapper.Map<UserVM>(e.Host),
                    Duration = e.Duration,
                    LocationVM = mapper.Map<LocationVM>(e.Location),
                    Images = mapper.Map<List<EventImagesVM>>(e.Images)

                }).FirstOrDefault(x => x.Id == id);
        }

        [HttpDelete, Route("{id}")]
        public void Delete(int id)
        {
            if (!appDb.Events.Any(x => x.Id == id))
                return;

            var del = appDb.Events.Find(id);
            appDb.Locations.Remove(appDb.Locations.FirstOrDefault(x => x.Id == del.LocationId));
            appDb.Remove(del);
            appDb.SaveChanges();
           
        }

        [HttpPost("search"), AllowAnonymous]
        public IEnumerable<EventVM> GetAll([FromBody] EventSearchVM eventSearch = null)
        {
            if (eventSearch == null)
            {
                return appDb.Events.Include(x => x.Location).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                .Select(e => new EventVM
                {
                    Id = e.Id,
                    Date = e.Date,
                    Duration = e.Duration,
                    LocationVM = mapper.Map<LocationVM>(e.Location),
                    EventDescription = e.EventDescription,
                    User = mapper.Map<UserVM>(e.Host),
                    Name = e.Name,
                    Price = e.Price,
                    Images = mapper.Map<List<EventImagesVM>>(e.Images)
                });
            }
            else
            {                if (string.IsNullOrEmpty(eventSearch.From)) eventSearch.From = DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd");
                if (string.IsNullOrEmpty(eventSearch.To)) eventSearch.To = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd");

                var a = appDb.Events.Include(x => x.Location).ThenInclude(x => x.City).ThenInclude(x => x.Country)
               .Where(e => eventSearch == null
               || (e.Location.City.Name.ToLower().Contains(eventSearch.Location.ToLower())
               || e.Location.Address.ToLower().Contains(eventSearch.Location.ToLower())
               || e.Location.City.Country.Name.ToLower().Contains(eventSearch.Location.ToLower()))
               && (e.Date >= DateTime.ParseExact(eventSearch.From, "yyyy-MM-dd", null) && e.Date <= DateTime.ParseExact(eventSearch.To, "yyyy-MM-dd", null)))
               .Select(e => new EventVM
               {
                   Id = e.Id,
                   Date = e.Date,
                   Duration = e.Duration,
                   LocationVM = mapper.Map<LocationVM>(e.Location),
                   EventDescription = e.EventDescription,
                   User = mapper.Map<UserVM>(e.Host),
                   Name = e.Name,
                   Price = e.Price,
                   Images = mapper.Map<List<EventImagesVM>>(e.Images)
               });
                return a;
            }
        }

        [HttpGet]
        [Route("user/{id}")]
        public List<EventVM> GetByUserId(string id)
        {


            var tempEvent = appDb.Events.Where(e => e.HostId == id).Include(x=> x.Location)
                .ThenInclude(x=>x.City).ThenInclude(x=>x.Country).Select(e =>
                 new EventVM()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Images = mapper.Map<List<EventImagesVM>>(e.Images),
                    EventDescription = e.EventDescription,
                    Date = e.Date,
                    Duration = e.Duration,
                    LocationVM = mapper.Map<LocationVM>(e.Location),
                    Price = e.Price,
                    User = mapper.Map<UserVM>(e.Host)
                }).ToList();


            return tempEvent;
        }

        [HttpPut, Route("{id}")]
        public void Update(int id, EventEditVM _event)
        {
            var temp = appDb.Events.FirstOrDefault(x => x.Id == id);

            temp.Location = mapper.Map<Location>(_event.LocationVM);
            temp.Name = _event.Name;
            temp.Price = _event.Price;
            temp.Date = _event.Date;
            temp.Duration = _event.Duration;
            temp.EventDescription = _event.EventDescription;

            appDb.Events.Update(temp);

            appDb.SaveChanges();
        }

       
    }
}

