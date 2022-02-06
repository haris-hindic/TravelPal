using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Database;
using TravelPalAPI.Helpers;
using TravelPalAPI.Models;
using TravelPalAPI.Repositories;
using TravelPalAPI.ViewModels.Event;

namespace TravelPalAPI.Controllers
{

   
    [Route("api/image/events")]
    [ApiController]

    public class EventImageController : ControllerBase
    {
        private readonly IEventImageRepository eventImageRepository;
       

        public EventImageController(IEventImageRepository eventImageRepository)
        {
            this.eventImageRepository = eventImageRepository;
        }

        [HttpPost]
        [Route("{id}")]
        public void Add(int id, [FromForm] EventImageCreationVM images)
        {
            eventImageRepository.Add(id, images);
        }

        [HttpDelete]
        [Route("{id}")]
        public void Delete(int id)
        {
            eventImageRepository.Delete(id);
        }
    }
}
