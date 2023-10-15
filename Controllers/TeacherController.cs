using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetAngularAuthWebApi.Context;
using NetAngularAuthWebApi.Models;
using NetAngularAuthWebApi.Models.Domain;
using NetAngularAuthWebApi.Models.Dto;

namespace NetAngularAuthWebApi.Controllers
{
    [Authorize(Roles = "headschool")]
    [Route("api/teacher")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly AppDbContext _teacher;

        public TeacherController(AppDbContext teacher)
        {
            _teacher = teacher;
        }

        [HttpPost("register-teacher")]
        public async Task<IActionResult> RegisterTeacher([FromBody] TeacherDTO teacherDTO){
            if(teacherDTO == null)
            return BadRequest();

            var inputTeacher = new Teacher{
                FullName = teacherDTO.FullName,
                    Age = teacherDTO.Age,
                    Address = teacherDTO.Address,
                    JoinDate = teacherDTO.JoinDate,
                    RolesId = teacherDTO.RolesId,
            };

            if(inputTeacher == null)
            return BadRequest();

            await _teacher.Teachers.AddAsync(inputTeacher);
            await _teacher.SaveChangesAsync();

            var response = new {
                Message = "Success Register",
                Data = inputTeacher
            };

            return Ok(response);
        }
        [HttpGet("get-teacher")]

        
        public async Task<IActionResult> GetTeacer()
        {
            // var clasSiswa = _teacher.Teachers.Join(
            //     _teacher.SchoolClasses,
            //     c => c.Id,
            //     (s, c) => new {
            //         Id = s.Id,
            //         FullName = s.FullName,
            //         Age = s.Age,
            //         Address = s.Address,
            //         JoinDate = s.JoinDate,
            //         NameOfSchool = c.NameOfSchool,
            //     }
            // ).ToList();
            var clasSiswa = _teacher.Teachers.ToList();
            if(clasSiswa == null)
            return NotFound();

            return Ok(clasSiswa);
        }


    }
}