using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class NotificationDto
    {
      
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public string Message { get; set; }
       
    }

    public class NotificationResponseDto
    {

        public Guid Id { get; set; }
        public Guid senderId { get; set; }
        public Guid ReceiverId { get; set; }
        public string Message { get; set; }
        public bool isRead { get; set; }

    }
}
