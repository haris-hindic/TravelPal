using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Models;
using TravelPalAPI.ViewModels.EventImages;
using TravelPalAPI.ViewModels.Identity;
using TravelPalAPI.ViewModels.Location;

namespace TravelPalAPI.ViewModels.Event
{
    public class EventVM
    {
        public int Id { get; set; }
        public UserVM User { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
        public string Duration { get; set; }
        public string EventDescription { get; set; }
        public LocationVM LocationVM { get; set; }
        public List<EventImagesVM> Images { get; set; }

    }
}
