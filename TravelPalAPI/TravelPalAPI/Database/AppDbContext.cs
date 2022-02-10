using Microsoft.AspNetCore.Identity;
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
        public DbSet<Location> Locations{ get; set; }
        public DbSet<Country> Countries{ get; set; }
        public DbSet<City> Cities{ get; set; }
        public DbSet<Accommodation> Accommodations{ get; set; }
        public DbSet<AccommodationDetails> AccommodationDetails{ get; set; }
        public DbSet<AccommodationImage> AccommodationImages{ get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventImages> EventImages{ get; set; }
        public DbSet<UserAccount> UserAccounts{ get; set; }
        public DbSet<Reservation> Reservations{ get; set; }
        public DbSet<Status> Statuses{ get; set; }
        public DbSet<PaymentInfo> PaymentInfos{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>().HasOne(x => x.PaymentInfo)
                .WithOne(x => x.Reservation).HasForeignKey<Reservation>(x => x.PaymentInfoId);


            base.OnModelCreating(modelBuilder);
        }
        
    }
}
