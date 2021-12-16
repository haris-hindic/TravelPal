using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPalAPI.ViewModels.Identity
{
    public class UserVM
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public int StaysListed { get; set; } = 1;
        public int EventsListed { get; set; } = 1;
        public bool IsAdmin { get; set; }
    }
}
