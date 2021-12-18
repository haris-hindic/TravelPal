using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPalAPI.Models
{
    public class EventImage
    {
        public int EventId { get; set; }
        public int ImageId { get; set; }
        public Image Image { get; set; }
    }
}
