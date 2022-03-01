using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Helpers.Pagination;
using TravelPalAPI.ViewModels.Event;

namespace TravelPalAPI.Repositories
{
    public interface IEventRepository
    {
         int Post(EventCreationVM eventCreation);
         EventVM Get(int id);
         void Delete(int id);
         Task<PagedList<EventVM>> GetAll(UserParams _params, EventSearchVM eventSearch = null);
         Task<PagedList<EventVM>> GetByUserId(string id, UserParams _params);
         void Update(int id, EventEditVM _event);

    }
}
