using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface INotificationRepository
    {
        Task<Notification> AddNotification(Notification notification);
        Task<List<Notification>> GetByReceiverId(Guid receiverId);
        Task<Notification?> MarkAsRead(Guid id);
        Task<Notification?> DeleteNotification(Guid id);

    }
}
