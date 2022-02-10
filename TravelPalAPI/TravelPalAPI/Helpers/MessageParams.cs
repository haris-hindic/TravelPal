using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Helpers.Pagination;

namespace TravelPalAPI.Helpers
{
    public class MessageParams : UserParams
    {
        public string UserId { get; set; }
        public string Container { get; set; } = "Unread";
    }
}
