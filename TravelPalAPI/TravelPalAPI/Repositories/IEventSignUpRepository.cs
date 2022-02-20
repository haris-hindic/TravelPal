using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Models;
using TravelPalAPI.ViewModels.EventSignUp;

namespace TravelPalAPI.Repositories.Implementation
{
    public interface IEventSignUpRepository
    {
        int Post(EventSignUpCreationVM eventSignUp);
        IEnumerable<EventSignUpVM> GetByUserId(string id);
        EventSignUpVM GetById(int id);
        void Delete(int id);
        public void CancelSignUp(int id);



    }
}
