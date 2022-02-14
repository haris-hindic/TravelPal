using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPalAPI.ViewModels.Accommodation
{
    public class AccommodationBasicVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public string Address { get; set;}
        public string Country { get; set;}
        public string City { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string UserImage { get; set; }
    }
}
