using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Models;

namespace TravelPalAPI.ViewModels.Accommodation
{
    public class AccommodationVM
    {
        //public Models.Accommodation Accommodation { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public Models.Location Location { get; set; }
        public AccommodationDetails AccommodationDetails { get; set; }
        public List<Image> Images { get; set; }
    }
}
