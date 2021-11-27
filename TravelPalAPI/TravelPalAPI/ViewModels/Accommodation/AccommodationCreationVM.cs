using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.ViewModels.Location;

namespace TravelPalAPI.ViewModels.Accommodation
{
    public class AccommodationCreationVM
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public LocationCreationVM Location { get; set; }
        public AccommodationDetailsCreationVM AccommodationDetails { get; set; }
    }
}
