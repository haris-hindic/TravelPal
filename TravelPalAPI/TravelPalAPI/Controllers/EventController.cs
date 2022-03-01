
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
using TravelPalAPI.Extensions;
using TravelPalAPI.Helpers;
using TravelPalAPI.Helpers.Pagination;
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
        public ActionResult<int>Post([FromBody] EventCreationVM eventCreation)
        {
            return eventRepository.Post(eventCreation);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<EventVM> Get(int id)
        {
            return eventRepository.Get(id);
        }

        [HttpDelete, Route("{id}")]
        public void Delete(int id)
        {
            eventRepository.Delete(id);
        }


        [HttpGet,AllowAnonymous]
        public async Task<ActionResult<PagedList<EventVM>>> GetAll([FromQuery] UserParams _params)
        {
            var events = await eventRepository.GetAll(_params);

            if (events == null)
                return NotFound();

          Response.AddPaginationHeader(events.CurrentPage, events.PageSize,
          events.TotalCount, events.TotalPages);

            return Ok(events);

        }

        [HttpGet]
        [Route("user/{id}")]
        public async Task<ActionResult<PagedList<EventVM>>> GetByUserId(string id, [FromQuery] UserParams _params)
        {
            var events = await eventRepository.GetByUserId(id, _params);

            Response.AddPaginationHeader(events.CurrentPage, events.PageSize,events.TotalCount, events.TotalPages);

            return Ok(events);
        }

        [HttpPut, Route("{id}")]
        public void Update(int id, EventEditVM _event)
        {
           eventRepository.Update(id, _event);
        }


        [HttpGet("search"), AllowAnonymous]
        public async Task<PagedList<EventVM>> Search([FromQuery] UserParams _params)
        {

            var dateFrom = Request.Query["dateFrom"];
            var dateTo = Request.Query["dateTo"];
            var location = Request.Query["location"];

            var events = await eventRepository.GetAll(_params, new EventSearchVM() { From = dateFrom, To = dateTo, Location = location});

            Response.AddPaginationHeader(events.CurrentPage, events.PageSize,
            events.TotalCount, events.TotalPages);

            return events;

        }

    }
}