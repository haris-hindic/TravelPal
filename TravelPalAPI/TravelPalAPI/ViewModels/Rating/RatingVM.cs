using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPalAPI.ViewModels.Rating
{
    public class RatingVM
    {
        public string User { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }
    }
}
