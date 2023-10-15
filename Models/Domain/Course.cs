using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NetAngularAuthWebApi.Models.Domain
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }

        public string VideoLink { get; set; }

        public string ImageBanner { get; set; }

        public string TypeCourse { get; set; } //kids / adults
        public ICollection<Student> Students { get; set; }
        // public int SchoolClassId { get; set; }
        // public SchoolClass SchoolClass { get; set; }
        public Guid? AuthorsId { get; set; }
        // public int SchoolClassId { get; set; }

        // public SchoolClass SchoolClass { get; set; }

        public Teacher Authors { get; set; }
        public Teacher Editor { get; set; }

        public int Price { get; set; }

    }
}