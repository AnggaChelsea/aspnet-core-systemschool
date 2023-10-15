using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetAngularAuthWebApi.Models.Dto
{
    public class StudentDTO
    {
        public string FullName { get; set; }
        public int Age { get; set; }
       public string Address { get; set; }
        public bool IsChangeSchool { get; set; }
        public string NameSchoolBefore { get; set; }
        public string Image { get; set; }
        public int NIM { get; set; }

        public bool IsActive { get; set; } = false;
        public DateTime RegisterTime { get; set; } = DateTime.UtcNow;


        public Guid? SchoolClassId { get; set; }
        public Guid RolesId { get; set; }

    }

    public class StudentDtoLogin{
        public string FullName { get; set;}
        public int NIM { get; set;}
        
    }

    public class JoinClass{
        public Guid SchoolClassId { get; set; }
    }

    public class ResponseDataLogin{
        // public string Email { get; set; }
        public string Token { get; set; }
    }
}