using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Responses<T>
    {
        public int StatuseCode {  get; set; }
        public string Message {  get; set; }
        public string Data { get; set; }
    }
}
