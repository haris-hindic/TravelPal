using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
    public class LocationController : ControllerBase
    {
        private readonly AppDbContext appDb;

        public LocationController(AppDbContext appDb)
        {
            this.appDb = appDb;
        }

        [HttpPost]
        public void Post(LocationCreationVM obj)
        {

            appDb.Locations.Add(new Location{ Country = obj.Country, City = obj.City, Address=obj.Address});
            appDb.SaveChanges();
        }

        [HttpGet]
        [Route("{id}")]
        public Location Get(int id)
        {
            return appDb.Locations.Find(id);
        }

        [HttpDelete]
        [Route("{id}")]
        public void Delete(int id)
        {
            var obj = appDb.Locations.Find(id);
            appDb.Remove(obj);
            appDb.SaveChanges();
        }

        [HttpGet]
        public object GetAll()
        {
            return appDb.Locations.ToArray();
        }

        [HttpPut]
        public ActionResult Update(Location location)
        {
            appDb.Locations.Update(location);
            appDb.SaveChanges();
            return Ok();
        }
    }
}
