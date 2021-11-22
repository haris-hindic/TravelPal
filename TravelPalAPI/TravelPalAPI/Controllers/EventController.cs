
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Context;
using TravelPalAPI.Models;
using TravelPalAPI.Models.ViewModels;

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

        public void Post([FromBody] EventVM _eventVM)
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
            return appDb.Events.Find(_id);
        }

        [HttpDelete]
        public void Delete(int _id)
        {
            var del = appDb.Events.Find(_id);
            appDb.Remove(del);
            appDb.SaveChanges();
        }

        [HttpGet]

        public object GetAll()
        {
            return appDb.Events.ToArray();

        }
        [HttpPut]

        public void Update(Event _event)
        {
            appDb.Events.Update(_event);
            appDb.SaveChanges();
        }
    }
}