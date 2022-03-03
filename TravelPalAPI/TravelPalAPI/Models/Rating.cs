using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPalAPI.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }

        public int AccommodationId { get; set; }
        public Accommodation Accommodation { get; set; }
        public string UserId { get; set; }
        public UserAccount User { get; set; }
    }
}
