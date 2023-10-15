using System;

namespace NetAngularAuthWebApi.Models.Domain
{
    public class CartCourse
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public int Total { get; set; }
        public Guid StudentId { get; set; }
        public Student Student { get; set; }
        
        public Guid CourseId { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}