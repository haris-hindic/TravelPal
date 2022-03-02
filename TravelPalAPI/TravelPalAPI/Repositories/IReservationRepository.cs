using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Helpers.Pagination;
using TravelPalAPI.ViewModels.Reservation;

namespace TravelPalAPI.Repositories
{
    public interface IReservationRepository
    {
        Task<ReservationUserInfoVM> UserInfo(string id);
        void Create(ReservationCreationVM reservation);
        Task<PagedList<ReservationVM>> ReservationsByUser(string id, UserParams userParams);
        Task<PagedList<ReservationVM>> ReservationsByHost(string id, UserParams userParams);
        void CancelReservation(int id);
        void ConfirmReservation(int id);
    }
}
