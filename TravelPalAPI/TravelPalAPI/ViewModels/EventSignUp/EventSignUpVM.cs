using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPalAPI.ViewModels.EventSignUp
{
    public class EventSignUpVM
    {
        public int Id { get; set; }
        public DateTime SignUpDate { get; set; }
        public float Price { get; set; }
        public string Event { get; set; }
        public string Status { get; set; }
        public DateTime EventDate { get; set; }
        public string Participant { get; set; }
    }
}
