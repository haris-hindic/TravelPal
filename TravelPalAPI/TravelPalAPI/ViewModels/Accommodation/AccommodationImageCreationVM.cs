using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPalAPI.ViewModels.Accommodation
{
    public class AccommodationImageCreationVM
    {
        public List<IFormFile> Images { get; set; }
        //public List<string> Images { get; set; }
    }
}
