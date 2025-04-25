using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class AddStatesDto
    {
        public string StateName {  get; set; }

        public Guid CountryId { get; set; }
         
    }

    public class UpdateStateDto
    {
        public string StateName { get; set; }
    }

    public class GetStateDto
    {
        public Guid Id { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }

        public Guid CountryId { get; set; }

    }


}
