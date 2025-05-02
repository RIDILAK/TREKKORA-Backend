using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class MessageCreateDto
    {
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
       
        public string MessageContent { get; set; }
    }

    public class MessageResponseDto
    {
        public Guid Id { get; set; }
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public string MessageContent { get; set; }
      
        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; }
    }
}
