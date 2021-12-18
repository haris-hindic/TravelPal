using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPalAPI.ViewModels.Event
{
    public class EventImageCreationVM
    {
        [Required]
        public List<IFormFile> Images { get; set; }

    }
}
