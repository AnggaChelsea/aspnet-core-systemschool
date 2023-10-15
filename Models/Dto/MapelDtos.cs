using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetAngularAuthWebApi.Models.Dto
{
    public class MapelDtos
    {
        [MaxLength(10)]
        [Required(ErrorMessage = "Dont be empty")]
        public string NamaMapel { get; set; }
        public Guid TeacherId { get; set; }
        public bool IsTugas { get; set; }
    }
}