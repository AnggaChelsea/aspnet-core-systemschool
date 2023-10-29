using System.Net.Mime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetAngularAuthWebApi.Context;
using NetAngularAuthWebApi.helpers;
using NetAngularAuthWebApi.Models;
using NetAngularAuthWebApi.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using NetAngularAuthWebApi.Models.Domain;
using Microsoft.AspNetCore.StaticFiles;
using AutoMapper;

namespace NetAngularAuthWebApi
{
    // [Authorize(Policy = "AllowStudentandTeacher")]
    [ApiController]
    [Route("api/school-class")]
    public class SchoolClassController : ControllerBase
    {
        private string _dataTampung;
        private readonly AppDbContext _schoolClass;
        // private readonly ILogger _logger;

        //inject readFile 
        private readonly FileExtensionContentTypeProvider _fileExtensionContentTypeProvider;

        private readonly IMapper _mapper;

        public SchoolClassController(AppDbContext schoolClass, FileExtensionContentTypeProvider fileExtensionContentTypeProvider = null, IMapper mapper = null)
        {
            _schoolClass = schoolClass;
            _fileExtensionContentTypeProvider = fileExtensionContentTypeProvider ?? throw new ArgumentNullException(nameof(_fileExtensionContentTypeProvider));
            _mapper = mapper;
            // _logger = logger;
        }

        [HttpPost("create-class")]
        // [Authorize(Roles = "admin")]
        public async Task<IActionResult> SchoolClassCreate([FromBody] SchoolClassDTO ScBody)
        {
            if (ScBody == null)
                return BadRequest(new { Message = "Data dont be null" });

            var bodyInput = new SchoolClass
            {
                NameOfSchool = ScBody.NameOfSchool,
                NameOfHeadSchool = ScBody.NameOfHeadSchool,
                ImageSchool = ScBody.ImageSchool,
                TimeStart = ScBody.TimeStart,
                TimeClose = ScBody.TimeClose,
            };
            if (bodyInput == null)
            {
                return BadRequest(new { Message = "Dont be empty" });
            }

            // var datacheck = _schoolClass.SchoolClasses.FirstOrDefault(x => x.NameOfSchool == bodyInput.NameOfSchool);
            // if (datacheck is not null)
            // {
            //     return BadRequest(new { Message = "name class already exists" });
            // }

            await _schoolClass.SchoolClasses.AddAsync(bodyInput);
            await _schoolClass.SaveChangesAsync();
            var response = new ResponseData();
            response.Message = "Success Created Data";
            var json = JsonSerializer.Serialize(response);
            return Ok(json);
        }

        [HttpDelete("delete-class/{id:int}")]
        public IActionResult SchoolClassDelete(Guid id)
        {
            var dataclass = _schoolClass.SchoolClasses.FirstOrDefault(sc => sc.Id == id);
            if (dataclass == null)
            {
                return NotFound(new { message = "School id not found" });
            }

            _schoolClass.Remove(dataclass);
            _schoolClass.SaveChanges();
            return Ok(new { Message = "deleted success" });

        }

        [HttpPatch("join-class/{id:int}")]
        public async Task<IActionResult> JoinClass(Guid id, [FromBody] JoinClass studentDTO)
        {
            var isExist = _schoolClass.Students.FirstOrDefault(e => e.Id == id);
            var sclass = _schoolClass.SchoolClasses.ToList();
            var responsse = new List<SchoolClass>();
            foreach (var student in sclass)
            {
                Console.WriteLine(student.Id);
                var dataStudent = _schoolClass.SchoolClasses.FirstOrDefault(s => s.NameOfSchool == student.NameOfSchool);
                responsse.Add(dataStudent);
            }
            if (isExist == null)
                return NotFound(new { Message = "Data Not Found" });

            var updateClass = new Student
            {
                SchoolClassId = studentDTO.SchoolClassId,
            };

            var response = new
            {
                Message = "Welcome in our class",
                Data = updateClass
            };

            _schoolClass.Students.Update(updateClass);
            _schoolClass.SaveChanges();
            return Ok(response);


        }

        [HttpGet("get-details/{id}")]
        public IActionResult GetSchoolDetails(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();
            var reslist = _schoolClass.SchoolClasses.Where(s => s.Id == id).ToList();
            if (reslist == null)
            {
                return NotFound();
            }

            return Ok(reslist);
        }



        [HttpGet("get-school-class")]
        public IActionResult GetSchoolClass(string sortOrder, string searchString, int page)
        {
            try
            {
                var CurrentFilter = sortOrder;
                if(searchString != null){
                    page = 1;
                }else{
                    searchString = CurrentFilter;
                }
                CurrentFilter = searchString;
                var dataclass = _schoolClass.SchoolClasses.ToList();
                var dataStudent = _schoolClass?.Students?.ToList();
                var teacherl = _schoolClass.Teachers.ToList();
                Console.WriteLine(teacherl);
                if (dataclass.Count == 0)
                    return Ok(new { Message = "Data Empty" });
                var response = new ResponseData();
                response.Message = "Success Get Data";
                response.Data = new List<SchoolClass>();
                if(!String.IsNullOrEmpty(searchString)){
                   var filter = dataclass.Where(s => s.NameOfHeadSchool.Contains(searchString) || s.NameOfSchool.Contains(searchString));
                }
                foreach (var datacls in dataStudent)
                {
                    var collecData = dataclass.FirstOrDefault(x => x.Id == datacls.SchoolClassId);
                    if (collecData != null)
                    {
                        response.Data.Add(collecData);
                    }
                }
                // Console.WriteLine(response.Data);
                var responseNew = new
                {
                    Message = "success get Data",
                    Data = dataclass,
                    StatusCode = 200
                    // StudentData = response.Data
                };
                return Ok(responseNew);
            }
            catch (Exception e)
            {
                // _logger.LogInformation(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("download-file/class")]
        public ActionResult GetFile(string fileId){
            var pathTo = "bankbjb.pdf";

            //check is file exists
            if(!System.IO.File.Exists(pathTo)){
                return NotFound(pathTo);
            }

            //check type of file content
            if(!_fileExtensionContentTypeProvider.TryGetContentType(
                pathTo, out var contentType
            )){
                contentType = "application/octet-stream";
            }


            var bytes = System.IO.File.ReadAllBytes(pathTo);
            return File(bytes, contentType, Path.GetFileName(pathTo));
        }

        private Object DoGetData(User user)
        {
            Console.WriteLine(user);
            return user;
        }

    }
}