using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NetAngularAuthWebApi.Models.Domain;

namespace NetAngularAuthWebApi.Models
{
    public class SchoolClass
    {
        public Guid Id { get; set; }
        public string NameOfSchool { get; set; }

        public string? NameOfHeadSchool { get; set; }
        public string ImageSchool { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeClose { get; set; }

        public ICollection<Student> Students { get; set; }
        public ICollection<Course> Courses { get; set; }
        public ICollection<Jadwal> Jadwals { get; set; }
    }
}
