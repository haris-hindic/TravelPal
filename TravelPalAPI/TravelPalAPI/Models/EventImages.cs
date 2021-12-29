﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPalAPI.Models
{
    public class EventImages
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}
