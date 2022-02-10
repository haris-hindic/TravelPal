using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPalAPI.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string SenderId { get; set; }
        public UserAccount Sender { get; set; }
        public string SenderUsername { get; set; }
        public string RecipientId { get; set; }
        public UserAccount Recipient { get; set; }
        public string RecipientUsername { get; set; }
        public string Content { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime MessageSent { get; set; } = DateTime.Now;
        public bool SenderDeleted { get; set; }
        public bool RecipientDeleted { get; set; }
    }
}
