using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPalAPI.ViewModels.Location
{
    public class LocationVM
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
    }
}
