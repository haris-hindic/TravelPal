using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Repositories;
using TravelPalAPI.ViewModels.Message;
using TravelPalAPI.Extensions;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using TravelPalAPI.Models;
using TravelPalAPI.Database;
using AutoMapper;
using TravelPalAPI.Helpers;

namespace TravelPalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly UserManager<UserAccount> _userManager;
        private readonly AppDbContext _dbContext;
        public IMapper _mapper;

        private readonly IMessageRepository _messageRepository;
        public MessageController(IMessageRepository messageRepository, UserManager<UserAccount> userManager,
            AppDbContext context, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _userManager = userManager;
            _dbContext = context;
            _mapper = mapper;
        }


        [HttpPost]
        public ActionResult<Message> CreateMessage(MessageCreationVM createMessage)
        {
            var recipient = _dbContext.UserAccounts.Find(createMessage.RecipientId);
            var sender = _dbContext.UserAccounts.Find(createMessage.SenderId);

            if(recipient == null || sender == null)
            {
                return Forbid("Error with ID");

            }
            if (recipient.Id == sender.Id)
            {
                return Forbid("Can't send message to yourself!");
            }

            var message = new Message()
            {
                Sender = sender,
                Recipient = recipient,
                SenderUsername = sender.UserName,
                RecipientUsername = recipient.UserName,
                Content = createMessage.Content
            };

            _messageRepository.AddMessage(message);

            _messageRepository.SaveAll();

            return Ok(_mapper.Map<MessageVM>(message));
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<MessageVM>>> GetMessagesForUser([FromQuery] MessageParams msgParams)
        {
            var user = _dbContext.UserAccounts.Where(x => x.Id == msgParams.UserId).FirstOrDefault();
            msgParams.UserId = user.Id;

            var messages = await _messageRepository.GetMessagesForUser(msgParams);

            Response.AddPaginationHeader(messages.CurrentPage, messages.PageSize, messages.TotalCount, messages.TotalPages);

            return messages;
        }

        [HttpGet("conversation/{userOne}/{userTwo}")]
        public ActionResult<IEnumerable<MessageVM>> GetConversation(string userOne, string userTwo)
        {
            var user = _dbContext.UserAccounts.FirstOrDefault(x => x.Id == userOne);

            return Ok(_messageRepository.GetConversation(user.Id, userTwo));
        }

        [HttpGet("readMessages")]
        public void ReadMessages(string id)
        {
            var user = _dbContext.UserAccounts.Where(x => x.Id == id).FirstOrDefault();
            var usrMsg = _dbContext.Messages.Where(x => x.RecipientId == user.Id && x.DateRead == null).ToList();

            foreach (var msg in usrMsg)
            {
                msg.DateRead = DateTime.Now;
            }
            _dbContext.SaveChanges();
        }
    }
}
    