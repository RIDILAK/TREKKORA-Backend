using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class CountryDto
    {
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
    }

    public class GetContryDto
    {
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public Guid Id { get; set; }
    }
}
