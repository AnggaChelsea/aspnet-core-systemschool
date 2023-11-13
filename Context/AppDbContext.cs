using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using NetAngularAuthWebApi.Models;
using NetAngularAuthWebApi.Models.Domain;
using NetAngularAuthWebApi.Models.Dto;
using NetAngularAuthWebApi.version.CqrsMediatR.Models;

namespace NetAngularAuthWebApi.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }

        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }

        public DbSet<CourseStudent> CourseStudents { get; set; }


        public DbSet<SchoolClass> SchoolClasses { get; set; }

        public DbSet<TimeSchool> TimeSchools { get; set; }

        public DbSet<Jadwal> Jadwals { get; set; }

        public DbSet<CartCourse> CartCourses { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Mapel> Mapels { get; set; }
        
        public DbSet<FileDetails> FileDetails { get; set; }
        public DbSet<MultiUploadFile> MultiUploadFiles { get; set; }


        //create fluent api
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<Course>().HasOne(c => c.Authors).WithMany(t => t.CourseWritenn);
            modelBuilder.Entity<CourseStudent>().HasKey(cs => new { cs.CourseId, cs.StudentId});
            modelBuilder.Entity<Student>().HasMany(x => x.Courses).WithMany(x => x.Students).UsingEntity<CourseStudent>(
                j => j.ToTable("CourseStudent")
            );
            modelBuilder.Entity<Teacher>().HasOne(x=>x.Roles).WithMany(y => y.Teachers);
            modelBuilder.Entity<Student>().HasOne(x => x.Roles).WithMany(y => y.Students);
            modelBuilder.Entity<Jadwal>().HasOne(x => x.SchoolClass).WithMany(y => y.Jadwals);
            // modelBuilder.Entity<Jadwal>().HasOne<SchoolClass>(x => x.SchoolClass).WithMany(y => y.Jadwal);
            modelBuilder.Entity<Mapel>().HasOne(x => x.Teachers).WithMany(s => s.Mapels);

        }

}

}
