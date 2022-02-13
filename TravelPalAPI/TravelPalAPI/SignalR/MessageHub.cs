using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Database;
using TravelPalAPI.Models;
using TravelPalAPI.Repositories;
using TravelPalAPI.ViewModels.Message;

namespace TravelPalAPI.SignalR
{
    public class MessageHub : Hub
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;
        private readonly AppDbContext _dbContext;


        public MessageHub(IMessageRepository messageRepository, IMapper mapper, AppDbContext appDbContext)
        {
            this._messageRepository = messageRepository;
            this._mapper = mapper;
            this._dbContext = appDbContext;
        }


        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();

            var currentUser = _dbContext.UserAccounts.Where(x => x.Id == httpContext.Request.Query["user"]
            .ToString()).SingleOrDefault();


            var otherUser = _dbContext.UserAccounts.Where(x => x.Id == httpContext.Request.Query["other"]
            .ToString()).SingleOrDefault();


            var groupName = GetGroupName(currentUser.UserName, otherUser.UserName);

            Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            var messages =  _messageRepository.GetConversation(currentUser.Id, otherUser.Id);

            Clients.Group(groupName).SendAsync("ReceiveMessageConversation", messages);
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            await base.OnDisconnectedAsync(ex);
        }

        public async Task SendMessage(MessageCreationVM messageCreationVM)
        {
            var recipient = _dbContext.UserAccounts.Find(messageCreationVM.RecipientId);
            var sender = _dbContext.UserAccounts.Find(messageCreationVM.SenderId);

            if (recipient == null || sender == null)
            {
                throw new HubException("Error with ID");

            }
            if (recipient.Id == sender.Id)
            {
                throw new HubException("Can't send message to yourself!");
            }

            var message = new Message()
            {
                Sender = sender,
                Recipient = recipient,
                SenderUsername = sender.UserName,
                RecipientUsername = recipient.UserName,
                Content = messageCreationVM.Content
            };

            _messageRepository.AddMessage(message);


            var group = GetGroupName(sender.UserName, recipient.UserName);

            Clients.Group(group).SendAsync("NewMessage", _mapper.Map<MessageVM>(message));
            
            _messageRepository.SaveAll();
        }


        private string GetGroupName(string currentUser, string otherUser)
        {
            var stringCompare = string.CompareOrdinal(currentUser, otherUser) < 0;
            return stringCompare ? $"{currentUser}-{otherUser}" : $"{otherUser}-{currentUser}";
        }
    }

    
}
