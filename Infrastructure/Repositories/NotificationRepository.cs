using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class NotificationRepository:INotificationRepository
    {
        private readonly AppDbContext _appDbContext;
        public NotificationRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Notification> AddNotification(Notification notification)
        {
            _appDbContext.Notification.Add(notification);
            await _appDbContext.SaveChangesAsync();
            return notification;
        }

        public async Task<List<Notification>> GetByReceiverId(Guid receiverId)
        {
          return await  _appDbContext.Notification.Where(c => c.ReceiverId == receiverId)
                .OrderByDescending(n => n.createdAt)
                .ToListAsync();
        }

        public async Task<Notification> MarkAsRead(Guid id)
        {
            var not = await _appDbContext.Notification.FirstOrDefaultAsync(c => c.Id == id);
            if (not == null)
            {
                return null;
            }
            not.isRead = true;
            await _appDbContext.SaveChangesAsync();
            return not;
        }
        public async Task <Notification> DeleteNotification(Guid id)
        {
            var noti= await _appDbContext.Notification.FirstOrDefaultAsync(c=>c.Id == id);
            if(noti == null)
            {
                return null;
            }
            _appDbContext.Notification.Remove(noti);
            await _appDbContext.SaveChangesAsync();
            return noti;
        }
    }


}
