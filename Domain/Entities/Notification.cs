using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Notification", Schema = "interactions")]
    public class Notification
    {
        public Guid Id { get; set; }
        public Guid senderId { get; set; }
        public Guid ReceiverId { get; set; }
        public string Message {  get; set; }
        public bool isRead {  get; set; }
        public DateTime createdAt { get; set; }
    }
}
