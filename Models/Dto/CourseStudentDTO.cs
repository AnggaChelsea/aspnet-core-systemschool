using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetAngularAuthWebApi.Models.Domain;

namespace NetAngularAuthWebApi.Models.Dto
{
    public class CourseStudentDTO
    {
        public Guid CourseId { get; set; }
        public Guid StudentId { get; set; }

        // public CourseStudentDTO(int counrseId, int studentId)
        // {
        //     CourseId = counrseId;
        //     StudentId = studentId;
        // }

    }

    public class CourseGetList{
        public Guid CourseId { get; set;}
        public string CourseTitle { get; set; }
        public int StudentCount { get; set; }
    }
}