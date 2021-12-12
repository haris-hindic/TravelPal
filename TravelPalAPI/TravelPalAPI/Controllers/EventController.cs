
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Database;
using TravelPalAPI.Models;
using TravelPalAPI.ViewModels.Event;
using TravelPalAPI.ViewModels.Location;

namespace TravelPalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly AppDbContext appDb;
        private readonly IMapper mapper;

        public EventController(AppDbContext appDb, IMapper imp)
        {
            this.appDb = appDb;
            mapper = imp;
        }


        [HttpPost]
        public void Post( EventCreationVM _eventCreation)
        {
            var _event = mapper.Map<Event>(_eventCreation);

            appDb.Events.Add(_event);

            appDb.SaveChanges();

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
                Duration = e.Duration,
                LocationVM = mapper.Map<LocationVM>(e.Location)
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


        [HttpGet]
        public IEnumerable<EventVM> GetAll()
        {
            return appDb.Events.Select(e => new EventVM
            {
                Id = e.Id,
                Date=e.Date,
                Duration=e.Duration,
                //Location=e.Location
                LocationVM = mapper.Map<LocationVM>(e.Location),
                EventDescription=e.EventDescription,
                Name=e.Name,
                Price=e.Price
            });

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