using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.ViewModels.Location;

namespace TravelPalAPI.ViewModels.Accommodation
{
    public class AccommodationCreationVM
    {
        [Required]
        public string Name { get; set; }
        [Required,Range(5.0,100000.0)]
        public double Price { get; set; }
        [Required]
        public string HostId { get; set; }
        [Required]
        public LocationCreationVM Location { get; set; }
        [Required]
        public AccommodationDetailsCreationVM AccommodationDetails { get; set; }
    }
}
