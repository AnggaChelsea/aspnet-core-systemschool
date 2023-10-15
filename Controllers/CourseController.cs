using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetAngularAuthWebApi.Context;
using NetAngularAuthWebApi.Models;
using NetAngularAuthWebApi.Models.Domain;
using NetAngularAuthWebApi.Models.Dto;

namespace NetAngularAuthWebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/course")]
    public class CourseController : ControllerBase
    {
        private readonly AppDbContext _course;

        private readonly DapperContext _dapper;
        public CourseController(AppDbContext course, DapperContext dapper){
            _course = course;
            _dapper = dapper;
        }


        //coba dapper
        [HttpPost("get-search")]
        public async Task<IActionResult> Coba([FromBody] CourseFilter course){
            // string query = "select * from dbo.Courses";
            // using(var connection = _dapper.CreateConnecting()){
            //     var user = await connection.QueryAsync<Course>(query);
            //     return Ok(user);
            // }
            if(course==null){
                return BadRequest();
            }
            //linq query
            var dataCourse = _course.Courses.Where(x => x.Title.Contains(course.Title)).ToList();
            if(dataCourse.Count==0){
                return NotFound(new {Message = "Data Not Found"});
            }
            return Ok(dataCourse);
        }

        [HttpPost("create-course")]
        public async Task<IActionResult> PostCourse([FromBody] CourseDTO bodyCourse){
            if(bodyCourse==null)
                return BadRequest();

            var inputCourse = new Course{
                Title = bodyCourse.Title,
                Description = bodyCourse.Description,
                ImageBanner = bodyCourse.ImageBanner,
                VideoLink = bodyCourse.VideoLink,
                TypeCourse = bodyCourse.TypeCourse,
                Content = bodyCourse.Content,
                AuthorsId = bodyCourse.AuthorsId,
            };
            if(inputCourse == null)
                return BadRequest();

            await _course.Courses.AddAsync(inputCourse);
            await _course.SaveChangesAsync();
            var response = new {
                Code = 200,
                Message = "Success Created",
                Data = inputCourse
            };
            return Ok(response);
            
        }
        [AllowAnonymous]
        [HttpPost("search-course")]
        public async Task<IActionResult> Search([FromBody] string searchName){
            if(string.IsNullOrEmpty(searchName))
                return BadRequest();

            var body = new Course{
                Title = searchName,
            };
            var dataCouse = _course.Courses.Where(x => x.Title.Contains(searchName)).ToList();
            Console.WriteLine(dataCouse);

            return Ok(dataCouse);

        }

        [HttpGet("test")]
        public IActionResult Test(){
            return Ok(new {Message = "hallo world"});
        }


        [HttpGet("get-course")]
        [Authorize(Roles = "Admin")]

         public async Task<IActionResult> GetCourse()
        {
            var siswaDb = _course.Courses.ToList();

            var clasSiswa = _course.Courses.Join(
                _course.Teachers,
                s => s.AuthorsId,
                c => c.Id,
                (s, c) => new {
                   Id = s.Id,
                   Title = s.Title,
                   Description = s.Description,
                   Content = s.Content,
                   ImageLink = s.ImageBanner,
                   Authors = c.FullName,
                   Editors = c.FullName,
                }
            ).ToList();

            if(clasSiswa == null)
            return NotFound();

            return Ok(clasSiswa);
        }

        [HttpDelete("course-delete")]
        public IActionResult DeleteCourse(Guid id){
            if(id == Guid.Empty){
                return BadRequest(new {Message = $"id {id} not found"});
            }

            var delete = _course.Courses.FirstOrDefault(x => x.Id == id);
            if(delete == null){
                return BadRequest();
            }

            _course.Courses.Remove(delete);
            _course.SaveChanges();

            return Ok(new {Message = "success delete"});

            
        }

        



    }
}