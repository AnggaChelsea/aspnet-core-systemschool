using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetAngularAuthWebApi.Context;
using NetAngularAuthWebApi.Models;

namespace NetAngularAuthWebApi.Repository
{
    public class StudentRepository : IStudentRepository, IDisposable
    {

        private readonly AppDbContext _studentsDbContext;
        private bool disposed = false;

        public StudentRepository(AppDbContext studentsDbContext)
        {
            _studentsDbContext = studentsDbContext;
        }

        void IStudentRepository.DeleteStudent(Guid studentId)
        {
            Student student = _studentsDbContext.Students.Find(studentId);
            _studentsDbContext.Remove(student);
        }

        void IDisposable.Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(!this.disposed){
                if(disposing){
                    _studentsDbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        Student IStudentRepository.GetStudentById(Guid studentId)
        {
            return _studentsDbContext.Students.Find(studentId);
        }

        IEnumerable<Student> IStudentRepository.GetStudents()
        {
            return _studentsDbContext.Students.ToList();
        }

        void IStudentRepository.InsertStudent(Student student)
        {
            
        }

        void IStudentRepository.SaveStudent()
        {
            _studentsDbContext.SaveChanges();
        }

        void IStudentRepository.UpdateStudent(Student student)
        {
            throw new NotImplementedException();
        }
    }
}