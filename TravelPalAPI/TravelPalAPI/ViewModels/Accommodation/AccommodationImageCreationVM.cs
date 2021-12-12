using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPalAPI.ViewModels.Accommodation
{
    public class AccommodationImageCreationVM
    {
        [Required]
        public List<IFormFile> Images { get; set; }
    }
}
