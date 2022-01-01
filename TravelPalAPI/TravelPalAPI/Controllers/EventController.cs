
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
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

namespace TravelPalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly AppDbContext appDb;
        private readonly IMapper mapper;

        public EventController(AppDbContext appDb, IMapper imp, IFileStorageService imgService)
        {
            this.appDb = appDb;
            mapper = imp;
        }


        [HttpPost]
        public ActionResult<int>Post([FromBody] EventCreationVM _eventCreation)
        {
            var _event = mapper.Map<Event>(_eventCreation);

            appDb.Events.Add(_event);

            appDb.SaveChanges();

            return _event.Id;

        }

        [HttpGet]
        [Route("{_id}")]
        public ActionResult<EventVM> Get(int _id)
        { 
            if (appDb.Events.Any(e => e.Id == _id))
                NotFound();

            return appDb.Events.Select(e => new EventVM
            {
                Id=e.Id,
                Name = e.Name,
                Date = e.Date,
                Price = e.Price,
                EventDescription = e.EventDescription,
                User = mapper.Map<UserVM>(e.Host),
                Duration = e.Duration,
                LocationVM = mapper.Map<LocationVM>(e.Location),
                Images = mapper.Map<List<EventImagesVM>>(e.Images)

            }).FirstOrDefault(x => x.Id == _id);
        }

        [HttpDelete, Route("{_id}")]
        public IActionResult Delete(int _id)
        {
            if (!appDb.Events.Any(x => x.Id == _id))
                return NotFound();
            var del = appDb.Events.Find(_id);
            appDb.Locations.Remove(appDb.Locations.FirstOrDefault(x => x.Id == del.LocationId));
            appDb.Remove(del);
            appDb.SaveChanges();
            return Ok($"{del.Name} was deleted");
        }


        [HttpGet,AllowAnonymous]
        public IEnumerable<EventVM> GetAll()
        {
            return appDb.Events.Select(e => new EventVM
            {
                Id = e.Id,
                Date=e.Date,
                Duration=e.Duration,
                LocationVM = mapper.Map<LocationVM>(e.Location),
                EventDescription=e.EventDescription,
                User = mapper.Map<UserVM>(e.Host),
                Name=e.Name,
                Price=e.Price,
                Images = mapper.Map<List<EventImagesVM>>(e.Images)

            });

        }

        [HttpGet]
        [Route("user/{id}")]
        public ActionResult<List<EventVM>> GetByUserId(string id)
        {
            if (!appDb.Events.Any(e => e.HostId == id))
                return NotFound("nema tog usera");

            var tempEvent = appDb.Events.Where(e => e.HostId == id).Select(e =>
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

        [HttpPut, Route("{_id}")]
        public void Update(int _id, EventEditVM _event)
        {
            var temp = appDb.Events.FirstOrDefault(x => x.Id == _id);

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