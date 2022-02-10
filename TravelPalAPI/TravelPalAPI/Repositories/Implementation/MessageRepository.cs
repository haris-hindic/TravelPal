﻿using AutoMapper;
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
                "Inbox" => messages.Where(u => u.Recipient.Id == msgParams.UserId),
                "Outbox" => messages.Where(u => u.Sender.Id == msgParams.UserId),
                _ => messages.Where(u => u.Recipient.Id == msgParams.UserId && u.DateRead == null)
            };

            var msg = messages.ProjectTo<MessageVM>(_mapper.ConfigurationProvider);

            return await PagedList<MessageVM>.Create(msg, msgParams.PageNumber, msgParams.PageSize); // -
        }

        public IEnumerable<MessageVM> GetConversation(string senderId, string recipientId)
        {
            var msg = _context.Messages.Include(x=> x.Sender).Include(x=> x.Recipient).Where(
            x => x.Recipient.Id == senderId && x.Sender.Id == recipientId
            || x.Sender.Id == senderId && x.Recipient.Id == recipientId).OrderBy(x=> x.MessageSent).ToList();

            var unreadMsg = msg.Where(x => x.DateRead == null && x.Recipient.Id == senderId).ToList();

            if(unreadMsg.Any())
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
    }
}