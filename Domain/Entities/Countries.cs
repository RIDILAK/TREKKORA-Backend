﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Countries", Schema = "locations")]
    public class Countries
    {
        public Guid Id { get; set; }
        public string CountryName { get; set; }

        public string CountryCode { get; set; }

        public DateTime CreatedAt { get; set; }
        public bool isDeleted {  get; set; }

        public ICollection<States> states { get; set; }
    }
}
