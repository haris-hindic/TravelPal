using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPalAPI.Models
{
    public class EventImages
    {
        public int EventId { get; set; }
        public Event Event { get; set; }
        public int ImageId { get; set; }
        public Image Image { get; set; }
    }
}
