using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Infrastructure.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly AppDbContext _context;

        public MessageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Message> AddMessage(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            return message;

        }
        public async Task<IEnumerable<Message>> GetMessage(Guid senderId, Guid recieverId)
        {
            return await _context.Messages
                .Where(m => (m.SenderId == senderId && m.ReceiverId == recieverId) ||
                (m.ReceiverId == recieverId && m.SenderId == m.SenderId))
                .OrderBy(m => m.SentAt)
                .ToListAsync();

        }

        public async Task<Message?> MarkAsRead(Guid id)
        {
            var mesg = await _context.Messages.FirstOrDefaultAsync(c => c.Id == id);
            if (mesg != null)
            {
                mesg.isRead = true;
                await _context.SaveChangesAsync();
                return mesg;
            }
            return null;
        }
    }
}
