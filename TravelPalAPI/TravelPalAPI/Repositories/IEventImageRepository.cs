using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.ViewModels.Event;

namespace TravelPalAPI.Repositories
{
    public interface IEventImageRepository
    {
         IActionResult Add(int id, [FromForm] EventImageCreationVM images);
         IActionResult Delete(int id);


    }
}
