using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPalAPI.ViewModels.EventSignUp
{
    public class EventSignUpVM
    {
        public DateTime SignUpDate { get; set; }
        public float Price { get; set; }
        public string Event { get; set; }
        public string Status { get; set; }
    }
}
