using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Models;
using TravelPalAPI.ViewModels.AccommodationDetails;
using TravelPalAPI.ViewModels.Identity;
using TravelPalAPI.ViewModels.Location;

namespace TravelPalAPI.ViewModels.Accommodation
{
    public class AccommodationVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public UserVM User { get; set; }
        public LocationVM Location { get; set; }
        public AccommodationDetailsVM AccommodationDetails { get; set; }
        public List<Image> Images { get; set; }
    }
}
