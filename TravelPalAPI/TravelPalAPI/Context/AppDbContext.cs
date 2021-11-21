using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Models;

namespace TravelPalAPI.Context
{
    public class AppDbContext : DbContext 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Event> Events{ get; set; }
        public DbSet<Location> Locations{ get; set; }
        public DbSet<Accommodation> Accommodations{ get; set; }
        public DbSet<AccommodationDetails> AccommodationDetails{ get; set; }
        
    }
}
