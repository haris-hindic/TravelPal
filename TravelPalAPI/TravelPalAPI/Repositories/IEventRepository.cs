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
         ActionResult<int> Post(EventCreationVM _eventCreation);
         ActionResult<EventVM> Get(int _id);
         IActionResult Delete(int _id);
         IEnumerable<EventVM> GetAll(EventSearchVM eventSearch = null);
         ActionResult<List<EventVM>> GetByUserId(string _id);
         void Update(int _id, EventEditVM _event);

    }
}
