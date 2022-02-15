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

            AddToGroup(Context, groupName, currentUser.UserName);

            var messages =  _messageRepository.GetConversation(currentUser.Id, otherUser.Id);

            Clients.Group(groupName).SendAsync("ReceiveMessageConversation", messages);

        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            await RemoveFromMessageGroup(Context.ConnectionId);
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

            var groupName = GetGroupName(sender.UserName, recipient.UserName);

            var group = _messageRepository.GetMessageGroup(groupName);

            if(group.Connections.Any(x=> x.Username == recipient.UserName))
            {
                message.DateRead = DateTime.Now;
            }

            _messageRepository.AddMessage(message);

            Clients.Group(groupName).SendAsync("NewMessage", _mapper.Map<MessageVM>(message));
            
            _messageRepository.SaveAll();
        }


        public  string GetGroupName(string currentUser, string otherUser)
        {
            var stringCompare = string.CompareOrdinal(currentUser, otherUser) < 0;
            return stringCompare ? $"{currentUser}-{otherUser}" : $"{otherUser}-{currentUser}";
        }

        public void AddToGroup(HubCallerContext context, string groupName, string userId)
        {
            var group = _messageRepository.GetMessageGroup(groupName);
            var connection = new Connection(context.ConnectionId, userId);

            if(group==null)
            {
                group = new Group(groupName);
                _messageRepository.AddGroup(group);
            }

            group.Connections.Add(connection);

            _dbContext.SaveChanges();
        }

        public async Task RemoveFromMessageGroup(string connectionId)
        {
            var connection = _messageRepository.GetConnection(connectionId);
            _messageRepository.RemoveConnection(connection);

            _dbContext.SaveChanges();
        }
    }

    
}
