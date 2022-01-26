using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace TravelPalAPI.ViewModels.Event
{
    public class EventSearchVM
    {
        public string? From { get; set; }
        public string Location { get; set; }
        public string? To { get; set; }
    }
}