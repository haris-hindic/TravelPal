
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Context;
using TravelPalAPI.Models;

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

        public void Post(Event _event)
        {
            appDb.Events.Add(_event);
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