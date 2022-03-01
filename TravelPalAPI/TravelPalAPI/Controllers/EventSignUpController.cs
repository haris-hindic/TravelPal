using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Extensions;
using TravelPalAPI.Helpers.Pagination;
using TravelPalAPI.Repositories.Implementation;
using TravelPalAPI.ViewModels.EventSignUp;

namespace TravelPalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventSignUpController : ControllerBase
    {
        private readonly IEventSignUpRepository _eventSignUpRepository;

        public EventSignUpController(IEventSignUpRepository eventSignUpRepository)
        {
            this._eventSignUpRepository = eventSignUpRepository;
        }
            
        [HttpDelete]
        public void Delete(int id)
        {
            _eventSignUpRepository.Delete(id);
        }

        [HttpGet("getByUserId/{id}")]
        public async Task<PagedList<EventSignUpVM>> GetByUserId(string id, [FromQuery] UserParams _params)
        {
            var events = await _eventSignUpRepository.GetByUserId(id, _params);

            Response.AddPaginationHeader(events.CurrentPage, events.PageSize,
            events.TotalCount, events.TotalPages);

            return events;
        }

        [HttpGet("{id}")]
        public EventSignUpVM GetById(int id)
        {
            return _eventSignUpRepository.GetById(id);
        }

        [HttpPost]
        public int Post([FromBody] EventSignUpCreationVM eventSignUp)
        {
            var tempEventSignUp = _eventSignUpRepository.Post(eventSignUp);

            return tempEventSignUp;
        }

        [HttpGet("cancelSignUp/{id}")]
        public void CancelSignUp(int id)
        {
            _eventSignUpRepository.CancelSignUp(id);
        }
    }
}
