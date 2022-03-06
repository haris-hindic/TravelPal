using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Models;
using TravelPalAPI.ViewModels.Accommodation;
using TravelPalAPI.ViewModels.Identity;

namespace TravelPalAPI.ViewModels.Rating
{
    public class RatingVM
    {
        public int Id { get; set; }
        public string User { get; set; }
        public AccommodationVM Accommodation { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }
    }
}
