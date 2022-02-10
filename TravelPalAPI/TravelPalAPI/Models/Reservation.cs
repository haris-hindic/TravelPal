using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPalAPI.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public float Price { get; set; }


        public string GuestId { get; set; }
        public UserAccount Guest { get; set; }

        public int AccommodationId { get; set; }
        public Accommodation Accommodation { get; set; }

        public int StatusId { get; set; }
        public Status Status { get; set; }

        public int PaymentInfoId { get; set; }
        public PaymentInfo PaymentInfo { get; set; }
    }
}
