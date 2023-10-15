using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetAngularAuthWebApi.Models.Dto
{
    public class SchoolClassDTO
    {
        public string NameOfSchool { get; set; }
        public string NameOfHeadSchool { get; set; }
        public string ImageSchool { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeClose { get; set; }
        
    }
}