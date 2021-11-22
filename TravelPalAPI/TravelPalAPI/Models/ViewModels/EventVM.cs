using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPalAPI.Models.ViewModels
{
    public class EventVM
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
        public string Duration { get; set; }
        public string EventDescription { get; set; }
        public LocationCreationVM LocationVM { get; set; }

    }
}
