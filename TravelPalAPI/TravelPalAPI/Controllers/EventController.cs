
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
using TravelPalAPI.Repositories;
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
        private IEventRepository eventRepository;

        public EventController(IEventRepository eventRepository)
        {
            this.eventRepository = eventRepository;
        }

        [HttpPost]
        public ActionResult<int>Post([FromBody] EventCreationVM _eventCreation)
        {
            return eventRepository.Post(_eventCreation);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<EventVM> Get(int id)
        {
            return eventRepository.Get(id);
        }

        [HttpDelete, Route("{_id}")]
        public void Delete(int _id)
        {
            eventRepository.Delete(_id);
        }


        [HttpGet,AllowAnonymous]
        public IEnumerable<EventVM> GetAll()
        {
            return eventRepository.GetAll();

        }

        [HttpGet]
        [Route("user/{id}")]
        public ActionResult<List<EventVM>> GetByUserId(string id)
        {
            return eventRepository.GetByUserId(id);
        }

        [HttpPut, Route("{id}")]
        public void Update(int id, EventEditVM _event)
        {
           eventRepository.Update(id, _event);
        }
        [HttpPost("search"), AllowAnonymous]
        public IEnumerable<EventVM> Search([FromBody] EventSearchVM eventSearch = null)
        {
            return eventRepository.GetAll(eventSearch);
        }


    }
}