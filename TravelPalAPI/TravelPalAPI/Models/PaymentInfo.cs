using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPalAPI.Models
{
    public class PaymentInfo
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public int CcNumber { get; set; }
        public DateTime ExpDate { get; set; }
        public string CcvCode { get; set; }

        public Reservation Reservation { get; set; }
    }
}
