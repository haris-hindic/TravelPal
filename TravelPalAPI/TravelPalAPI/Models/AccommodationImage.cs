using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPalAPI.Models
{
    public class AccommodationImage
    {
        public int AccommodationId { get; set; }
        //public Accommodation Accommodation { get; set; }
        public int ImageId { get; set; }
        public Image Image { get; set; }
    }
}
