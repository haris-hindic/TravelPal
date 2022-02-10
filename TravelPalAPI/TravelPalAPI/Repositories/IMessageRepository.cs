using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Helpers;
using TravelPalAPI.Helpers.Pagination;
using TravelPalAPI.Models;
using TravelPalAPI.ViewModels.Message;

namespace TravelPalAPI.Repositories
{
    public interface IMessageRepository
    {
        void AddMessage(Message message);
        void DeleteMessage(Message message);
        Message GetMessage(int id);
        Task<PagedList<MessageVM>> GetMessagesForUser(MessageParams msgParams);
        IEnumerable<MessageVM> GetMessageThread(int currentUserId, int recipientId);
        void SaveAll();

    }
}
