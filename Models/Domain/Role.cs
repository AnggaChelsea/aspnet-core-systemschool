using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace NetAngularAuthWebApi.Models.Domain
{
    public class Roles
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameFileUpload { get; set; }
        public Collection<Teacher> Teachers { get; set; }
        public Collection<Student> Students { get; set; }
    }
}