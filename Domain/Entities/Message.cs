using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Message
    {
        public Guid Id { get; set; }
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }



        public string MessageContent { get; set; }
        public DateTime SentAt { get; set; }
        public bool isRead { get; set; }
    }
}
