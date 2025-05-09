﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("WishList", Schema = "useractions")]
    public class WishList
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public User User { get; set; }
        public Guid PlaceId { get; set; }

        public Place Place { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
