using System.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NetAngularAuthWebApi.Context;
using NetAngularAuthWebApi.helpers;
using NetAngularAuthWebApi.Models;
using NetAngularAuthWebApi.Models.Domain;
using NetAngularAuthWebApi.Models.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using NetAngularAuthWebApi.Repository;
using Sieve.Services;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
namespace NetAngularAuthWebApi.Controllers
{

    [ApiController]
    [Route("api/student")]
    public class StudentController : ControllerBase
    {

        private const string TokenScret = "TokenScreetAndMustBeScreetDontToSHowAnywone";
        private static readonly TimeSpan TokenLifetime = TimeSpan.FromHours(1);
        private readonly AppDbContext _student;
        private readonly AppDbContext _scClass;

        private readonly AppDbContext _courses;


        private readonly IConfiguration _config;
        private readonly DapperContext _context;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public StudentController(AppDbContext student, AppDbContext scClass, AppDbContext courses, IConfiguration config, DapperContext context, SieveProcessor sieveProcessor, IWebHostEnvironment webHostEnvironment)
        {
            _student = student;
            _scClass = scClass;
            _courses = courses;
            _config = config;
            _context = context;
            this._sieveProcessor = sieveProcessor;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpPost("uploadImageRetrive")]

        public async Task<IActionResult> UploadImageRetrives([FromForm] List<IFormFile> _uploadFiles)
        {
            bool Result = false;
            try
            {
                // var _uploadFiles = Request.Form.Files;
                foreach (IFormFile source in _uploadFiles)
                {
                    string filename = source.FileName;
                    string FilePath = GetFilePath(filename);
                    if (!System.IO.Directory.Exists(FilePath))
                    {
                        System.IO.Directory.CreateDirectory(FilePath);
                    }
                    string imagepath = FilePath + "\\image.png";
                    if (System.IO.File.Exists(imagepath))
                    {
                        System.IO.File.Delete(imagepath);
                    }
                    using (FileStream stream = System.IO.File.Create(imagepath))
                    {
                        await source.CopyToAsync(stream);
                        Result = true;
                    }
                }
            }
            catch (Exception e)
            {

            }
            return Ok(Result);
        }
       [HttpDelete("delete-image")]
        public ResponseType RemoveImage(string code){
            try{
          string Filepath =  GetFilePath(code);
          string imagepath = Filepath + "\\image.png";
           if (System.IO.File.Exists(imagepath))
            {
                System.IO.File.Delete(imagepath);
            }
            }catch(Exception ex){}
            return new ResponseType {Result = "pass", KyValue = code};
        }

         [NonAction]
        private string GetFilePath(string Filecode)
        {
            return _webHostEnvironment.WebRootPath + "\\Uploads\\" + Filecode;
        }
        [NonAction]

        private string GetFilePathByProducts(string Filecode)
        {
            string ImageUrl = string.Empty;
            string HostUrl = "https://localhost:7023/";
            string filepath = GetFilePath(Filecode);
            string imagepath = Filecode + "\\image.png";
            if (!System.IO.File.Exists(imagepath))
            {
                ImageUrl = HostUrl + "/Uploads/common/noimage.png";
            }
            else
            {
                ImageUrl = HostUrl + "/Uploads/" + Filecode + "/image.png";
            }
            return ImageUrl;
        }



        [AllowAnonymous]
        [HttpGet("get-students")]
        public IActionResult Siswa([FromQuery] string? fullName, int page = 1, int pageSize = 10)
        {
            var siswaDb = _student.Students.ToList();
            var fullNameFilter = siswaDb.Where(e =>e.FullName.Contains(fullName)).ToList();
                var clasSiswa = _student.Students.Join(
                               _student.SchoolClasses,
                               s => s.SchoolClassId,
                               c => c.Id,
                               (s, c) => new
                               {
                                   Id = s.Id,
                                   FullName = s.FullName,
                                   Age = s.Age,
                                   Nim = s.NIM,
                                   Image = GetFilePathByProducts(s.NIM.ToString()),
                                   Address = s.Address,
                                   IsChangeSchool = s.IsChangeSchool,
                                   NameSchoolBefore = s.NameSchoolBefore,
                                //    Image = s.Image,
                                   IsActive = s.IsActive,
                                   RegisterTime = s.RegisterTime,
                                   NameOfSchool = c.NameOfSchool,
                                   RolesId = s.RolesId
                               }
                           )
                           .OrderByDescending(s => s.FullName)
                           .ThenByDescending(s => s.FullName)
                           .ToList();
                var dataRoles = siswaDb.OrderByDescending(x => x.RolesId == x.Id);
                var totalData = siswaDb.Count();
                var dataWithpagination = siswaDb.Skip((page - 1) * pageSize)
                .Take(pageSize).ToList();
                var url = Request.Path.Value;
                var totalPage = (int)Math.Ceiling((double)totalData / pageSize);
                var baseUrl = $"{Request.Scheme}://{Request.Host}{url}";
                var response = new
                {
                    Message = "success get data",
                    siswaJoined = dataWithpagination,
                    totalData = dataWithpagination.Count(),
                    page = page,
                    pageSize = pageSize,
                    NextPage = page + 1,
                    PrevPage = page - 1,
                    NextPageUrl = page > 1 ? $"{baseUrl}?page={page - 1}&pageSize={pageSize}" : null,
                    PrevPageUrl = page < totalPage ? $"{baseUrl}?page={page + 1}&pageSize={pageSize} " : null,
                };

                if (clasSiswa == null)
                    return NotFound(new { Message = "data empty" });


                return Ok(response);
            

        }



        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _student.Students.FindAsync(id);

            if (data == null)
            {
                return NotFound();
            }

            _student.Students.Remove(data);
            await _student.SaveChangesAsync();
            return Ok(new { Message = "data deleted" });
        }

        [HttpPatch("{id:int}")]
        public IActionResult Edit(int id, [FromBody] Student student)
        {
            var isExist = _student.Students.Find(id);
            if (isExist == null)
            {
                return NotFound();
            }
            //update the data
            isExist.FullName = student.FullName;

            isExist.SchoolClassId = student.SchoolClassId;
            _student.Students.Update(isExist);
            _student.SaveChanges();

            var updated = new Student
            {
                FullName = student.FullName,
                Age = student.Age,
                Address = student.Address,
                IsChangeSchool = student.IsChangeSchool,
                NameSchoolBefore = student.NameSchoolBefore,
                Image = student.Image,
                SchoolClassId = student.SchoolClassId,
            };
            var response = new
            {
                message = "updated success",
                data = updated
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var dataStudent = _student.Students.Find(id);
            Console.WriteLine(dataStudent);
            if (dataStudent == null)
            {
                return BadRequest(new { Message = "data kosong" });
            }
            return Ok(dataStudent);
        }



        [HttpPost("login-users")]
        public IActionResult Login([FromBody] StudentDtoLogin login)
        {
            if (login == null)
                return BadRequest();


            var dataStudent = _student.Students.FirstOrDefault(x => x.FullName == login.FullName && x.NIM == login.NIM);
            var token = GenerateToken(dataStudent);
            var response = new
            {
                Message = "Success Login",
                Data = dataStudent,
                Token = token
            };
            // if(dataStudent.NIM == null)
            // return BadRequest(new {Message = "nim not valid"});

            return Ok(response);
        }



        //read course
        [HttpPost("course-read")]
        public async Task<IActionResult> PostReadCourse(CourseStudentDTO courseStudentDTO)
        {
            var course = await _student.Courses.FindAsync(courseStudentDTO.CourseId);
            var student = await _student.Students.FindAsync(courseStudentDTO.StudentId);

            if (course == null || student == null)
                return BadRequest(new { message = "invalid course or studentid" });

            var courseStudent = new CourseStudent
            {
                Course = course,
                Student = student
            };
            if (course is not null || student is not null || courseStudent.Student == student)
                return Ok(new { Message = " your arleady readit " });

            await _student.CourseStudents.AddAsync(courseStudent);
            await _student.SaveChangesAsync();

            return Ok(new { Message = "Course student relationship added successfully.", Data = courseStudent });
        }

        [HttpGet("get-course-student")]
        public IActionResult GetCourseStudent()
        {
            var courseStudent = _student.CourseStudents.ToList();
            var studentList = _student.Students.ToList();
            var courseList = _student.Courses.ToList();
            var teacherList = _student.Teachers.ToList();
            var modifiedCourseLiist = new List<CourseStudent>();
            var innerjoins = from s in studentList
                             join cs in courseStudent
            on s.Id equals cs.StudentId
                             join c in courseList on cs.CourseId equals c.Id
                             select new
                             {
                                 FullName = s.FullName,
                                 Title = c.Title,
                             };
            Console.WriteLine(innerjoins);
            var reponse = new ResponseData
            {
                Message = "Course gets",
                // Data = innerjoins,
            };
            // var courseStudentDTOs = _courses.CourseStudents.Select(cs => new CourseStudentDTO
            // {
            //     CourseId = cs.CourseId,
            //     StudentId = cs.StudentId
            // }).ToList();

            return Ok(innerjoins);


        }

        // public string GetToken(Student student) {
        //     var claims = new[] {
        //         new Claim(JwtRegisteredClaimNames.Sub, _config["jwt:Subject"]),
        //         new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //         new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
        //         new Claim("UserId", student.Id.ToString()),
        //         new Claim("UserName", student.FullName.ToString()),
        //     };

        //     var scurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        //     var credintials = new SigningCredentials(scurityKey, SecurityAlgorithms.HmacSha256);

        //     var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], null, expires: DateTime.Now.AddDays(1),
        //     signingCredentials: credintials
        //     );
        //     return new JwtSecurityTokenHandler().WriteToken(token);


        // }

        [HttpGet("GetToken")]
        public string CreateAccessToken()
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "Tom"),
                    new Claim(ClaimTypes.Email, "Tom@gmail.com")
            };

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("CJKFOGk-9E0aI8Gv09mD-8utzSyLQx_yrJKi1fXc6Y7CeYszLzcmMA2C0_Ej3K7BQdsCW9zoqW3a-5L1ZNRytFC0BeA6dZLsCjoTrFoI9guwvEmJ0gbN9yHQ0fDYbkwGUyJbP6eNEzKbWHMarSx7RWGKaGsxy0qguEMSO3OUWU8"));
            var jwtInfo = new JwtSecurityToken(
                    issuer: "localhost",
                    audience: "audience1",
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(4)),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                    );
            var Token = new JwtSecurityTokenHandler().WriteToken(jwtInfo);
            return Token;
        }

        [HttpGet("apiss")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public string Test()
        {
            return "You have pass the bearer";
        }

        private string GenerateToken(Student student)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, student.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("Id", student.Id.ToString()),
                new Claim("FullName", student.FullName.ToString()),
                new Claim("Age", student.Age.ToString()),
                new Claim("NIM", student.NIM.ToString()),
            };

            var scurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]));
            var credintials = new SigningCredentials(scurityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["JwtSettings:Issuer"], _config["JwtSettings:Audience"], null, expires: DateTime.Now.AddDays(1),
            signingCredentials: credintials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}