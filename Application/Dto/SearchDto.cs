using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class SearchDto
    {
        public string Query { get; set; }
    }

    public class SearchResultDto
    {
        public string Type { get; set; } 
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public Guid Id { get; set; }
    }
}
