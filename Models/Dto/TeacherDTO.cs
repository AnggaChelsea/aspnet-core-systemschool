using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetAngularAuthWebApi.Models.Dto
{
    public class TeacherDTO
    {
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Address { get;}
        public DateTime JoinDate { get; set; }
        public Guid RolesId { get; set; } 
        public string Image { get; set; }
    }
}