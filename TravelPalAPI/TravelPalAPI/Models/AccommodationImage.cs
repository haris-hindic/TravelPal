using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPalAPI.Models
{
    public class AccommodationImage
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public int AccommodationId { get; set; }
        public Accommodation Accommodation { get; set; }
        public int MyProperty { get; set; }
    }
}
