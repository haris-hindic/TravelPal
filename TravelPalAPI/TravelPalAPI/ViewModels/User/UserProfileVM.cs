using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Helpers.Pagination;
using TravelPalAPI.ViewModels.Accommodation;
using TravelPalAPI.ViewModels.Event;

namespace TravelPalAPI.ViewModels.User
{
    public class UserProfileVM
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool EmailVerified { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberVerified { get; set; }
        public string Picture { get; set; }
        public IEnumerable<AccommodationBasicVM> Accommodations { get; set; }
        public IEnumerable<EventVM> Events { get; set; }

    }
}
