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
         int Post(EventCreationVM _eventCreation);
         EventVM Get(int _id);
         void Delete(int _id);
         IEnumerable<EventVM> GetAll(EventSearchVM eventSearch = null);
         List<EventVM> GetByUserId(string _id);
         void Update(int _id, EventEditVM _event);

    }
}
