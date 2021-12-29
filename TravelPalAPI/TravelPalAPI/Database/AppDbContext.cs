using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Models;

namespace TravelPalAPI.Database
{
    public class AppDbContext : IdentityDbContext 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Event> Events { get; set; }
        public DbSet<Location> Locations{ get; set; }
        public DbSet<Accommodation> Accommodations{ get; set; }
        public DbSet<AccommodationDetails> AccommodationDetails{ get; set; }
        public DbSet<AccommodationImage> AccommodationImages{ get; set; }
        public DbSet<EventImages> EventImages{ get; set; }
        public DbSet<UserAccount> UserAccounts{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }
        
    }
}
