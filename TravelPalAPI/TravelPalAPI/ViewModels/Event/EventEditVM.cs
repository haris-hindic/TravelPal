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
        public string Duration { get; set; }
        [Required, MaxLength(5)]
        public string EventDescription { get; set; }
        [Required]
        public Models.Location LocationVM { get; set; }
    }
}
