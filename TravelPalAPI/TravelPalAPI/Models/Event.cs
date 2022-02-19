using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPalAPI.Models
{
    public class Event
    {
        public int Id { get; set; }
        // User
        public string HostId { get; set; }
        public UserAccount Host  { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public string EventDescription { get; set; }
        public int LocationId { get; set; }
        [ForeignKey(nameof(LocationId))]
        public Location Location { get; set; }
        public List<EventImages> Images { get; set; }
        public List<EventSignUp> EventSignUps { get; set; }

    }
}
