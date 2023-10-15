using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetAngularAuthWebApi.Models.Domain
{
    public class CourseStudent
    {
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
        public Guid StudentId { get; set; }
        public Student Student { get; set; }
    }
}