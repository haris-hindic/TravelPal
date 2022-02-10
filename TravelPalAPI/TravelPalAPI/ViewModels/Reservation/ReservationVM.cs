using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPalAPI.ViewModels.Reservation
{
    public class ReservationVM
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public float Price { get; set; }
        public string Accommodation { get; set; }
        public string Status { get; set; }
    }
}
