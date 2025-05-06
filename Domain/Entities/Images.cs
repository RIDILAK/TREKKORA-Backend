using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Images",Schema ="Documents")]
    public class Images
    {
        public Guid Id { get; set; }
        public Guid PlaceId { get; set; }
        public byte[] ImageData { get; set; }
        public string FileName {  get; set; }
        public string ContentType {  get; set; }
        public Place Place { get; set; }
    }
}
