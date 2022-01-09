using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Models;
using TravelPalAPI.ViewModels.AccommodationDetails;
using TravelPalAPI.ViewModels.Location;

namespace TravelPalAPI.ViewModels.Accommodation
{
    public class AccommodationEditVM
    {
        [Required]
        public string Name { get; set; }
        [Required,Range(1, 100000)]
        public double Price { get; set; }
        [Required]
        public string Description { get; set; }
        [Required, Range(1, 50)]
        public int Rooms { get; set; }
        [Required, Range(1, 50)]
        public int Capacity { get; set; }
        [Required]
        public LocationVM Location { get; set; }
        [Required]
        public AccommodationDetailsVM AccommodationDetails { get; set; }
    }
}
