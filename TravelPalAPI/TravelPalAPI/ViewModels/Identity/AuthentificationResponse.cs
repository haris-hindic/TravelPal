using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPalAPI.ViewModels.Identity
{
    public class AuthentificationResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
