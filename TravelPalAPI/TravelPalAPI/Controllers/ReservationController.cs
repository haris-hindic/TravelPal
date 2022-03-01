using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Database;
using TravelPalAPI.Extensions;
using TravelPalAPI.Helpers.Pagination;
using TravelPalAPI.Models;
using TravelPalAPI.Repositories;
using TravelPalAPI.ViewModels.Reservation;
using TravelPalAPI.ViewModels.User;

namespace TravelPalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsVerified")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationRepository reservationRepository;

        public ReservationController(IReservationRepository reservationRepository)
        {
            this.reservationRepository = reservationRepository;
        }

        [HttpGet("user-info/{id}")]
        public async Task<ActionResult<ReservationUserInfoVM>> GetUserInfo(string id)
        {
            var userInfo = await reservationRepository.UserInfo(id);

            return userInfo != null ? userInfo : BadRequest("User not foud!"); 
        }

        [HttpPost("create")]
        public ActionResult CreateReservation([FromBody]ReservationCreationVM reservation)
        {
            reservationRepository.Create(reservation);
            return Ok();
        }

        [HttpGet("user-reservations/{id}")]
        public async Task<ActionResult<PagedList<ReservationVM>>> ReservationsByUser(string id,[FromQuery] UserParams userParams)
        {
            var reservations = await reservationRepository.ReservationsByUser(id, userParams);

            Response.AddPaginationHeader(reservations.CurrentPage, reservations.PageSize,
                reservations.TotalCount, reservations.TotalPages);

            return reservations != null ? reservations : BadRequest("User not found!");
        }

        [HttpGet("host-reservations/{id}")]
        public async Task<ActionResult<PagedList<ReservationVM>>> ReservationsByHost(string id, [FromQuery] UserParams userParams)
        {
            var reservations = await reservationRepository.ReservationsByHost(id, userParams);

            Response.AddPaginationHeader(reservations.CurrentPage, reservations.PageSize,
                reservations.TotalCount, reservations.TotalPages);

            return reservations != null ? reservations : BadRequest("User not found!");
        }

        [HttpGet("cancel/{id}")]
        public ActionResult CancelReservation(int id)
        {
            reservationRepository.CancelReservation(id);
            return Ok();
        }

        [HttpGet("confirm/{id}")]
        public ActionResult ConfirmReservation(int id)
        {
            reservationRepository.ConfirmReservation(id);
            return Ok();
        }
    }
}
