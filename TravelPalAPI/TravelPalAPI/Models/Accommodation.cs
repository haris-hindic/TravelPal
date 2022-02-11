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
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int Rooms{ get; set; }
        public int Capacity { get; set; }

        public int LocationId { get; set; }
        public Location Location { get; set; }

        public string HostId { get; set; }
        public UserAccount Host { get; set; }

        public AccommodationDetails AccommodationDetails { get; set; }
        public List<AccommodationImage> AccommodationImages { get; set; }
        public List<Reservation> Reservations{ get; set; }
    }
}
