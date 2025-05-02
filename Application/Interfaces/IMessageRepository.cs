using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IMessageRepository
    {
        Task<Message> AddMessage(Message message);
        Task<IEnumerable<Message>> GetMessage(Guid senderId, Guid recieverId);
        Task<Message?> MarkAsRead(Guid id);
    }
}
