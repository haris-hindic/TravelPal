using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Database;
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
        public IEnumerable<EventSignUpVM> GetAll()
        {
            var eventSignUps = _dbContext.EventSignUps.Include(x => x.Event).Include(x=>x.Status).ToList();

            return _mapper.Map<IEnumerable<EventSignUpVM>>(eventSignUps);
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
            var eventSignUp = _mapper.Map<EventSignUp>(eventSignUpCreation);
            eventSignUp.StatusId = _dbContext.Statuses.SingleOrDefault(x => x.Description == "Active").Id;

            _dbContext.EventSignUps.Add(eventSignUp);
            _dbContext.SaveChanges();
            /*_dbContext.EventSignUps.Add(new EventSignUp()
            {
                EventId = eventSignUpCreation.EventId,
                EventParticipantId = eventSignUpCreation.EventParticipantId,
                PaymentInfo = _mapper.Map<PaymentInfo>(eventSignUpCreation.PaymentInfo),
                Price = eventSignUpCreation.Price,
                SignUpDate = eventSignUpCreation.SignUpDate
            });
            */
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
    }
}
