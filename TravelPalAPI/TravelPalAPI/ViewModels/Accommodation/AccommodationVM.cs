using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Models;
using TravelPalAPI.ViewModels.AccommodationDetails;
using TravelPalAPI.ViewModels.AccommodationImage;
using TravelPalAPI.ViewModels.Identity;
using TravelPalAPI.ViewModels.Location;
using TravelPalAPI.ViewModels.Reservation;

namespace TravelPalAPI.ViewModels.Accommodation
{
    public class AccommodationVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int Rooms { get; set; }
        public int Capacity { get; set; }
        public UserVM User { get; set; }
        public LocationVM Location { get; set; }
        public AccommodationDetailsVM AccommodationDetails { get; set; }
        public List<AccommodationImageVM> Images { get; set; }
        public List<ReservationDatesVM> DateReserved { get; set; }
    }
}
