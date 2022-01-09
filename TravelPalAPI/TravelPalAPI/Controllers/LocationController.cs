using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Database;
using TravelPalAPI.Models;
using TravelPalAPI.ViewModels.Location;

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
        public void Add(List<string> c,int id)
        {
            foreach (var city in c)
            {
                appDb.Cities.Add(new City { CountryId = id, Name = city });

            }
            appDb.SaveChanges();
        }

        [HttpGet("countries")]
        public IEnumerable<CountryVM> GetCountries()
        {
             
            return appDb.Countries.OrderBy(x=>x.Name).Select(x=> new CountryVM
            {
                Id = x.Id,
                Name=x.Name
            }).ToArray();
        }

        [HttpGet("cities/{id}")]
        public IEnumerable<CityVM> GetCities(int id)
        {

            return appDb.Cities.Where(x=>x.CountryId==id).Select(x => new CityVM
            {
                Id = x.Id,
                Name = x.Name
            });
        }
    }
}
