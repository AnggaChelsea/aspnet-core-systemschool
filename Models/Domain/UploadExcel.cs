using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetAngularAuthWebApi.Models.Domain
{
    public class UploadExcelStudent
    {
        [Key]
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Roll { get; set; }
        public string Image { get; set; }

    }
}