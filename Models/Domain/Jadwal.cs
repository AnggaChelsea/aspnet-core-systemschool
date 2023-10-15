using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetAngularAuthWebApi.Models.Domain
{
    public class Jadwal
    {
        public Guid Id { get; set; }
        public DateTime JamMapel { get; set; }
        public Guid KelasId { get; set; }
        public SchoolClass SchoolClass { get; set; }
        public Guid MapelId { get; set; }
        public Mapel Mapels { get; set; }
    }
}