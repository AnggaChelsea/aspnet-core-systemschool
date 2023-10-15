using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NetAngularAuthWebApi.Models.Domain
{
    public class Teacher
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public DateTime JoinDate { get; set; }
        public Guid RolesId { get; set; }
        public string Image { get; set; }
        public Roles Roles { get; set; }
        [ForeignKey("Authors")]
        public ICollection<Course> CourseWritenn { get; set; }
        public ICollection<Mapel> Mapels { get; set; }
    }
}