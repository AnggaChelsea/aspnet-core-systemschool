using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetAngularAuthWebApi.Models.Dto
{
    public class JadwalDtos
    {
        public DateTime JamMapel { get; set; }
        public Guid KelasId { get; set; }
        public Guid MapelId { get; set; }
    }
}