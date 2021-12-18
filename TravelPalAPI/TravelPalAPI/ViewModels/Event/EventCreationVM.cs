using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.ViewModels.Location;

namespace TravelPalAPI.ViewModels.Event
{
    public class EventCreationVM
    {
        [Required]
        public string Name { get; set; }
        [Required, Range(5,200)]
        public double Price { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Duration { get; set; }
        [Required]
        public string EventDescription { get; set; }
        [Required]
        public LocationCreationVM LocationVM { get; set; }

    }
}
