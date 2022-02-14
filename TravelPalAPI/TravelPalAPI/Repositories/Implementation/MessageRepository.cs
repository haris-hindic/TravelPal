using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Database;
using TravelPalAPI.Helpers;
using TravelPalAPI.Helpers.Pagination;
using TravelPalAPI.Models;
using TravelPalAPI.ViewModels.Message;

namespace TravelPalAPI.Repositories.Implementation
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IMapper _mapper;

        public AppDbContext _context { get; }

        public MessageRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public void AddMessage(Message message)
        {
            _context.Messages.Add(message);
        }

        public void DeleteMessage(Message message)
        {
            _context.Messages.Remove(message);
        }

        public Message GetMessage(int id)
        {
            return  _context.Messages.Find(id);
        }

        public async Task<PagedList<MessageVM>> GetMessagesForUser(MessageParams msgParams)
        {

          
            var messages = _context.Messages
                .OrderByDescending(m => m.MessageSent)
                .AsQueryable();

            messages = msgParams.Container switch
            {
                "Inbox" => messages.Where(u => u.Recipient.Id == msgParams.UserId && u.RecipientDeleted == false),
                "Outbox" => messages.Where(u => u.Sender.Id == msgParams.UserId && u.SenderDeleted == false),
                _ => messages.Where(u => u.Recipient.Id == msgParams.UserId && u.DateRead == null && u.RecipientDeleted==false)
            };

            var msg = messages.ProjectTo<MessageVM>(_mapper.ConfigurationProvider);

            return await PagedList<MessageVM>.Create(msg, msgParams.PageNumber, msgParams.PageSize); 
        }

        public IEnumerable<MessageVM> GetConversation(string userId, string recipientId)
        {
            var msg = _context.Messages
                .Include(x=> x.Sender).
                Include(x=> x.Recipient)
                .Where(x => (x.Recipient.Id == userId && x.RecipientDeleted == false && x.Sender.Id == recipientId) || (x.RecipientId == recipientId
            && x.Sender.Id == userId && x.SenderDeleted == false)).OrderBy(x=> x.MessageSent).ToList();

            var unreadMsg = msg.Where(x => x.DateRead == null && x.Recipient.Id == userId).ToList();

            if (unreadMsg.Any())
            {
                foreach (var item in unreadMsg)
                {
                    item.DateRead = DateTime.Now;
                }

                _context.SaveChanges();
            }



            return _mapper.Map<IEnumerable<MessageVM>>(msg);
        }

        public void SaveAll()
        {
            _context.SaveChanges(); 
        }

        public void AddGroup(Group group)
        {
            _context.Groups.Add(group);
        }

        public void RemoveConnection(Connection connection)
        {
            _context.Connections.Remove(connection);
        }

        public Connection GetConnection(string connectionId)
        {
            return _context.Connections.Find(connectionId);
        }

        public Group GetMessageGroup(string groupName)
        {
            return _context.Groups.Include(x => x.Connections).SingleOrDefault(x => x.Name == groupName);

        }
    }
}
