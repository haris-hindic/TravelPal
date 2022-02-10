using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPalAPI.ViewModels.Reservation
{
    public class ReservationCreationVM
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public float Price { get; set; }
        public string GuestId { get; set; }
        public int AccommodationId { get; set; }
    }
}
