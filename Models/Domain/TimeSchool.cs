using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetAngularAuthWebApi.Models.Domain
{
    public class TimeSchool
    {
        public Int64 Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

    public class Branch : TimeSchool
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}