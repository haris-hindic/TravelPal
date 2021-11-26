
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
        public ActionResult Post(EventCreationVM _eventVM)
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
            return Ok("Event added");
        }

        [HttpGet]
        [Route("{_id}")]
        public Event Get(int _id)
        {
            return appDb.Events.Include(x => x.Location).FirstOrDefault(x => x.Id == _id);
        }

        [HttpDelete]
        public ActionResult Delete(int _id)
        {
            var del = appDb.Events.Find(_id);
            return Ok($"{del.Name} was deleted");
            appDb.Remove(del);
            appDb.SaveChanges();
        }


        [HttpGet]
        public IEnumerable<EventVM> GetAll()
        {
            return appDb.Events.Select(e => new EventVM
            {
                Id = e.Id,
                Date=e.Date,
                Duration=e.Duration,
                Location=e.Location,
                EventDescription=e.EventDescription,
                Name=e.Name,
                Price=e.Price
            });

        }

        [HttpPut, Route("update/{_id}")]
        public ActionResult Update(int _id, EventEditVM _event)
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
            return Ok("Event updated");
        }
       
        
    }
}