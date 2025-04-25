using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class States
    {
        public Guid Id { get; set; }
        public string StateName { get; set; }
        public Guid CountryId { get; set; }

        public bool IsDeleted {  get; set; }

        public DateTime CreatedAt { get; set; }

        public Countries Countries { get; set; }

        public ICollection<Place> places { get; set; }


    }
}
