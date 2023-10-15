using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NetAngularAuthWebApi.Models.Domain;
using Sieve.Attributes;

namespace NetAngularAuthWebApi.Models
{
    public class Student
    {
        
        [Key]
        public Guid Id { get; set; }
        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public bool IsChangeSchool { get; set; }
        public string NameSchoolBefore { get; set; }
        public string Image { get; set; }
        public int NIM { get; set; }

        public bool IsActive { get; set; }
        public DateTime RegisterTime { get; set; } = DateTime.UtcNow;

        //navigate property
        public Guid? SchoolClassId { get; set; }
        public Guid? RolesId { get; set; }
        public Roles Roles { get; set; }
        public SchoolClass SchoolClass { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
