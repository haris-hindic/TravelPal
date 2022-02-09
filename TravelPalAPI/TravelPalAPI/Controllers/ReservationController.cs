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
using TravelPalAPI.Models;
using TravelPalAPI.ViewModels.Reservation;
using TravelPalAPI.ViewModels.User;

namespace TravelPalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsVerified")]
    public class ReservationController : ControllerBase
    {
        private readonly AppDbContext appDb;
        private readonly UserManager<UserAccount> userManager;
        private readonly IMapper mapper;

        public ReservationController(AppDbContext appDb,UserManager<UserAccount> userManager,IMapper mapper)
        {
            this.appDb = appDb;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        [HttpGet("user-info/{id}")]
        public async Task<ActionResult<ReservationUserInfoVM>> GetUserInfo(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null) return BadRequest("User not found!");

            return new ReservationUserInfoVM
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateReservation([FromBody]ReservationCreationVM reservation)
        {
            var obj = mapper.Map<Reservation>(reservation);

            obj.Date = DateTime.Now;
            obj.StatusId = (await appDb.Statuses.FirstOrDefaultAsync(x => x.Description == "Active")).Id;

            appDb.Reservations.Add(obj);
            await appDb.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("user-reservations/{id}")]
        public async Task<ActionResult<List<ReservationVM>>> ReservationsByUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null) return BadRequest("User not found!");

            var reservations = await appDb.Reservations.Include(x=>x.Accommodation).Include(x=>x.Status).Where(x => x.GuestId == id).ToListAsync();

            return Ok(mapper.Map<List<ReservationVM>>(reservations));
        }

        [HttpGet("stay-reservations/{id}")]
        public async Task<ActionResult<List<ReservationVM>>> ReservationsByAccommodation(int id)
        {
            if (appDb.Accommodations.Any(x=>x.Id==id)) return BadRequest("Accommodation not found!");

            var reservations = await appDb.Reservations.Include(x => x.Accommodation).Include(x => x.Status).Where(x => x.AccommodationId == id).ToListAsync();

            return Ok(mapper.Map<List<ReservationVM>>(reservations));
        }

        [HttpPut("cancel/{id}")]
        public async Task<ActionResult> CreateReservation(int id)
        {
            var obj = await appDb.Reservations.FirstOrDefaultAsync(x=>x.Id==id);

            obj.StatusId = (await appDb.Statuses.FirstOrDefaultAsync(x => x.Description == "Cancelled")).Id;

            appDb.Reservations.Update(obj);
            await appDb.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("confirm/{id}")]
        public async Task<ActionResult> ConfirmReservation(int id)
        {
            var obj = await appDb.Reservations.FirstOrDefaultAsync(x => x.Id == id);

            obj.StatusId = (await appDb.Statuses.FirstOrDefaultAsync(x => x.Description == "Completed")).Id;

            appDb.Reservations.Update(obj);
            await appDb.SaveChangesAsync();
            return Ok();
        }
    }
}
