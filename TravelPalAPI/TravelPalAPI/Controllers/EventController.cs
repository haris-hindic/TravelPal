
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

namespace TravelPalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly AppDbContext appDb;

        public EventController(AppDbContext appDb)
        {
            this.appDb = appDb;
        }


        [HttpPost]
        public void Post(EventCreationVM _eventVM)
        {
            appDb.Events.Add(new Event
            {
                Date = _eventVM.Date,
                Duration = _eventVM.Duration,
                EventDescription = _eventVM.EventDescription,
                Location = new Location
                {
                    Country = _eventVM.LocationVM.Country,
                    City = _eventVM.LocationVM.City,
                    Address = _eventVM.LocationVM.Address
                },
                Name = _eventVM.Name,
                Price = _eventVM.Price
            });

            appDb.SaveChanges();
        }

        [HttpGet]
        [Route("{_id}")]
        public Event Get(int _id)
        {
            return appDb.Events.Include(x => x.Location).FirstOrDefault(x => x.Id == _id);
        }

        [HttpDelete]
        public void Delete(int _id)
        {
            var del = appDb.Events.Find(_id);
            appDb.Remove(del);
            appDb.SaveChanges();
        }


        [HttpGet]
        public IEnumerable<Event> GetAll()
        {
            return appDb.Events.Include(x => x.Location).ToArray();

        }

        [HttpPut, Route("update/{_id}")]
        public void Update(int _id, EventEditVM _event)
        {
            var temp = appDb.Events.FirstOrDefault(x => x.Id == _id);

            temp.Location = _event.Location;
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