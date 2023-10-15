using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetAngularAuthWebApi.Models.Dto
{
    public class CartCourseDto
    {
        public Guid StudentId { get; set; }
        public int Quantity { get; set; }
        public int Total { get; set; }
        public Guid CourseId { get; set; }
    }
}