using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetAngularAuthWebApi.Models.Domain
{
    public class Mapel
    {
        public Guid Id { get; set; }
        public string NamaMapel { get; set; }
        public Guid TeacherId { get; set; }
        public Teacher Teachers { get; set; }
        public bool IsTugas { get; set; }
    }
}