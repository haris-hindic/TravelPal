using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPalAPI.ViewModels.Rating
{
    public class RatingCreationVM
    {
        public string UserId { get; set; }
        public int AccommodationId { get; set; }
        [Range(1,5)]
        public int Rate { get; set; }
        public string Comment { get; set; }
    }
}
