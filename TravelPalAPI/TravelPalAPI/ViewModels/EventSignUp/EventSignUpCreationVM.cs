using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.ViewModels.PaymentInfo;

namespace TravelPalAPI.ViewModels.EventSignUp
{
    public class EventSignUpCreationVM
    {
        public string EventParticipantId { get; set; }
        public int EventId { get; set; }
        public float Price { get; set; }
        public DateTime SignUpDate { get; set; }
        public PaymentInfoCreationVM PaymentInfo { get; set; }
    }
}
