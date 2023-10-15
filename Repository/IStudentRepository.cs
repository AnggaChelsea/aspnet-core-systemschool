using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetAngularAuthWebApi.Models;

namespace NetAngularAuthWebApi.Repository
{
    public interface IStudentRepository: IDisposable
    {
        IEnumerable<Student> GetStudents();
        Student GetStudentById(Guid studentId);
        void InsertStudent(Student student);
        void DeleteStudent(Guid studentId);
        void UpdateStudent(Student student);
        void SaveStudent();


    }
}