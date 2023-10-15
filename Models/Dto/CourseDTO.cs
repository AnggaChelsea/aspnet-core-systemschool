using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetAngularAuthWebApi.Models.Dto
{
    public class CourseDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string VideoLink { get; set; }
        public string ImageBanner { get; set; }
        public string TypeCourse { get; set; }

        public Guid AuthorsId { get; set; }

    }

    public class CourseFilter{
        public string Title { get; set;}
    }
}