using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPalAPI.Models
{
    public class EventSignUp
    {
        public int Id { get; set; }
        public DateTime SignUpDate { get; set; } = DateTime.Now;
        public float Price { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
        public string EventParticipantId { get; set; }
        public UserAccount EventParticipant { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public int PaymentInfoId { get; set; }
        public PaymentInfo PaymentInfo { get; set; }
    }
}
