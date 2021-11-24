using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.ViewModels.Location;

namespace TravelPalAPI.ViewModels.Event
{
    public class EventCreationVM
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
        public string Duration { get; set; }
        public string EventDescription { get; set; }
        public LocationCreationVM LocationVM { get; set; }

    }
}
