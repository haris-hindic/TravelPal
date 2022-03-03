using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Database;
using TravelPalAPI.Helpers.Pagination;
using TravelPalAPI.Models;
using TravelPalAPI.ViewModels.EventSignUp;
using TravelPalAPI.ViewModels.PaymentInfo;

namespace TravelPalAPI.Repositories.Implementation
{
    public class EventSignUpRepository : IEventSignUpRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;


        public EventSignUpRepository(AppDbContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }
        public async Task<PagedList<EventSignUpVM>> GetByUserId(string id, UserParams _params)
        {
            var eventSignUps = _dbContext.EventSignUps.Include(x => x.Event).Include(x=>x.Status).Where(x=>x.EventParticipantId == id).Select(x=> new EventSignUpVM()
            {
                Price = x.Price,
                EventDate = x.SignUpDate,
                Event = x.Event.Name,
                Id = x.Id,
                SignUpDate = x.SignUpDate,
                Status = x.Status.Description
            });

            return await PagedList<EventSignUpVM>.Create(eventSignUps,
                _params.PageNumber, _params.PageSize);
        }

        public EventSignUpVM GetById(int id)
        {
            var eventSignUp = _dbContext.EventSignUps.Include(x => x.Event).Include(x => x.Status).SingleOrDefault(x => x.Id == id);

            if (eventSignUp == null)
                return null;

           
            return _mapper.Map<EventSignUpVM>(eventSignUp);
        }

        public int Post(EventSignUpCreationVM eventSignUpCreation)
        {
            if (!_dbContext.Events.Any(x => x.Id == eventSignUpCreation.EventId))
                return -1;


            var eventSignUp = _mapper.Map<EventSignUp>(eventSignUpCreation);
            eventSignUp.StatusId = _dbContext.Statuses.SingleOrDefault(x => x.Description == "Active").Id;

            _dbContext.EventSignUps.Add(eventSignUp);
            _dbContext.SaveChanges();
          
            return eventSignUp.Id;
        }
        public void Delete(int id)
        {
            var eventSignUp = _dbContext.EventSignUps.SingleOrDefault(x => x.Id == id);

            if (eventSignUp == null)
                return;

            _dbContext.EventSignUps.Remove(eventSignUp);

            _dbContext.SaveChanges();
        }

        public void CancelSignUp(int id)
        {
            var signUp = _dbContext.EventSignUps.Where(x => x.Id == id).SingleOrDefault();

            if (signUp == null)
                return;

            signUp.Status = _dbContext.Statuses.SingleOrDefault(x => x.Description == "Cancelled");

            _dbContext.SaveChanges();
        }

        public async Task<PagedList<EventSignUpVM>> GetHostedSignUps(string id, int eventId, UserParams _params)
        {
            var user = _dbContext.UserAccounts.SingleOrDefault(x => x.Id == id);

            if (user == null)
                return null;

            IQueryable<EventSignUpVM> signUps;

            if (eventId == -1)
            {
                  signUps = _dbContext.EventSignUps.Include(x => x.Event).Include(x => x.Status)
                    .Where(x => x.Event.HostId == id)
                    .Select(x => new EventSignUpVM()
                    {
                        Id = x.Id,
                        Event = x.Event.Name,
                        Status = x.Status.Description,
                        EventDate = x.Event.Date,
                        Price = x.Price,
                        SignUpDate = x.SignUpDate,
                        Participant = x.EventParticipant.FirstName + ' ' + x.EventParticipant.LastName
                    });
            }
            else
            {
                signUps = _dbContext.EventSignUps.Include(x => x.Event).Include(x => x.Status)
                    .Where(x => x.Event.HostId == id && eventId == x.EventId)
                    .Select(x => new EventSignUpVM()
                    {
                        Id = x.Id,
                        Event = x.Event.Name,
                        Status = x.Status.Description,
                        EventDate = x.Event.Date,
                        Price = x.Price,
                        SignUpDate = x.SignUpDate,
                        Participant = x.EventParticipant.FirstName + ' ' + x.EventParticipant.LastName
                    });
            }

            return await PagedList<EventSignUpVM>.Create(signUps,
                _params.PageNumber, _params.PageSize);

        }

    }
}
