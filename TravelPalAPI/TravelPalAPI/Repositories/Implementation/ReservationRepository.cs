using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Database;
using TravelPalAPI.Helpers.Pagination;
using TravelPalAPI.Models;
using TravelPalAPI.ViewModels.Reservation;

namespace TravelPalAPI.Repositories.Implementation
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly AppDbContext appDb;
        private readonly UserManager<UserAccount> userManager;
        private readonly IMapper mapper;

        public ReservationRepository(AppDbContext appDb, UserManager<UserAccount> userManager, IMapper mapper)
        {
            this.appDb = appDb;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public void CancelReservation(int id)
        {
            var obj = appDb.Reservations.FirstOrDefault(x => x.Id == id);

            obj.StatusId = (appDb.Statuses.FirstOrDefault(x => x.Description == "Cancelled")).Id;

            appDb.Reservations.Update(obj);
             appDb.SaveChanges();
        }

        public void ConfirmReservation(int id)
        {
            var obj = appDb.Reservations.FirstOrDefault(x => x.Id == id);

            obj.StatusId = (appDb.Statuses.FirstOrDefault(x => x.Description == "Completed")).Id;

            appDb.Reservations.Update(obj);
            appDb.SaveChanges();
        }

        public void Create(ReservationCreationVM reservation)
        {
            var obj = mapper.Map<Reservation>(reservation);

            obj.Date = DateTime.Now;
            obj.StatusId = ( appDb.Statuses.FirstOrDefault(x => x.Description == "Active")).Id;

            appDb.Reservations.Add(obj);
            appDb.SaveChanges();
        }

        public async Task<PagedList<ReservationVM>> ReservationsByHost(string id, UserParams userParams)
        {
            var host = await userManager.FindByIdAsync(id);

            if (host == null) return null;

            var reservations = appDb.Reservations.Include(x => x.Accommodation).Include(x => x.Status).Where(x => x.Accommodation.HostId == id)
                .Select(x => new ReservationVM
                {
                    Id = x.Id,
                    Accommodation = x.Accommodation.Name,
                    Start = x.Start,
                    End = x.End,
                    Price = x.Price,
                    Status = x.Status.Description
                });

            return await PagedList<ReservationVM>.Create(reservations, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<PagedList<ReservationVM>> ReservationsByUser(string id, UserParams userParams)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null) return null;

            var reservations = appDb.Reservations.Include(x => x.Accommodation).Include(x => x.Status).Where(x => x.GuestId == id)
                    .Select(x => new ReservationVM
                    {
                        Id = x.Id,
                        Accommodation = x.Accommodation.Name,
                        Start = x.Start,
                        End = x.End,
                        Price = x.Price,
                        Status = x.Status.Description
                    });

            return await PagedList<ReservationVM>.Create(reservations, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<ReservationUserInfoVM> UserInfo(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null) return null;

            return new ReservationUserInfoVM
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };
        }
    }
}
