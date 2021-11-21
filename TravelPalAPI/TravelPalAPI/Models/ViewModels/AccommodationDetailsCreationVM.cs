using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPalAPI.Models.ViewModels
{
    public class AccommodationDetailsCreationVM
    {
        public bool Parking { get; set; }
        public bool WiFi { get; set; }
        public bool Shower { get; set; }
        public bool Minibar { get; set; }
        public bool AirConditioning { get; set; }
        public bool Safe { get; set; }
        public bool Dryer { get; set; }
        public bool FlatScreenTV { get; set; }
        public bool PetFriendly { get; set; }
        public bool BBQ { get; set; }
        public bool Refrigerator { get; set; }
        public bool Balcony { get; set; }
        public bool MosquitoNet { get; set; }
    }
}
