using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.ViewModels.Event;

namespace TravelPalAPI.Repositories
{
    public interface IEventRepository
    {
         int Post(EventCreationVM eventCreation);
         EventVM Get(int id);
         void Delete(int id);
         IEnumerable<EventVM> GetAll(EventSearchVM eventSearch = null);
         List<EventVM> GetByUserId(string id);
         void Update(int id, EventEditVM _event);

    }
}
