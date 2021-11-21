using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPalAPI.Models
{
    public class Accommodation
    {
        public int Id { get; set; }
        //public int HostID { get; set; }
        //public Host Host { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        //public IEnumerable<AccommodationImage> EventImages { get; set; }

        public int LocationId { get; set; }
        [ForeignKey(nameof(LocationId))]
        public Location Location { get; set; }
        public int AccommodationDetailsId { get; set; }
        public AccommodationDetails AccommodationDetails { get; set; }
    }
}
