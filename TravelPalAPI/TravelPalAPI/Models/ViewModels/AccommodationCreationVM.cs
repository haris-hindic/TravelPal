using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPalAPI.Models.ViewModels
{
    public class AccommodationCreationVM
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public LocationCreationVM LocationCreation { get; set; }
        public AccommodationDetailsCreationVM AccommodationDetailsCreation { get; set; }
    }
}
