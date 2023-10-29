using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetAngularAuthWebApi.Models.Dto
{
    public class SchoolClassDTO
    {
        [Required]
        [MaxLength(4)]
        public string NameOfSchool { get; set; }
        public string NameOfHeadSchool { get; set; }
        public string ImageSchool { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeClose { get; set; }
        
    }
}