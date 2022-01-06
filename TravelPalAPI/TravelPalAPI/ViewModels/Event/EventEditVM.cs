using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.ViewModels.Location;
namespace TravelPalAPI.ViewModels.Event
{
    public class EventEditVM
    {
        [Required]
        public string Name { get; set; }
        [Required,Range(5, 200)]
        public double Price { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int Duration { get; set; }
        [Required, MaxLength(30)]
        public string EventDescription { get; set; }
        [Required]
        public LocationVM LocationVM { get; set; }
    }
}
