using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPalAPI.ViewModels.Location
{
    public class LocationVM
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Country { get; set; } 
        public string City { get; set; }
        [Required]
        public int CityId { get; set; }
        public int CountryId { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }
    }
}
