using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NetAngularAuthWebApi.Context;
using NetAngularAuthWebApi.Models.Domain;
using NetAngularAuthWebApi.Models.Dto;
using NetAngularAuthWebApi.Services;
using NetAngularAuthWebApi.Services.Student;

namespace NetAngularAuthWebApi.Controllers
{
    [Authorize(Policy = "AllowStudentandTeacher")]
    [ApiController]
    [Route("api/mapel")]
    public class MapelController : ControllerBase
    {

        private readonly AppDbContext _dbContext;
        private readonly IMailService _imailService;
        // private readonly ILogger _logger;

        private readonly IStudentService _studentService;

        public MapelController(AppDbContext dbContext, IMailService imailService, IStudentService studentService)
        {
            _dbContext = dbContext;
            _imailService = imailService;
            _studentService = studentService;
            // _logger = logger;
        }

        [HttpPost("create-mapel")]
        public async Task<IActionResult> CreateMapel([FromBody] MapelDtos mapelDtos){
            try{
                if(mapelDtos == null){
                    return BadRequest();
                }
                if(ModelState.IsValid){
                    return BadRequest();
                }
                var body = new Mapel{
                    NamaMapel = mapelDtos.NamaMapel,
                    TeacherId = mapelDtos.TeacherId,
                    IsTugas = false,
                };
                var dataresult = _dbContext.Mapels.FirstOrDefault(x => x.NamaMapel == mapelDtos.NamaMapel);
                if(dataresult is not null){
                    return BadRequest(new {Message = "Mapel Name already exists"});
                }
                await _dbContext.Mapels.AddAsync(body);
                await _dbContext.SaveChangesAsync();
                return Ok(new {Message = "Mapel Name created successfully"});
            }catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-mapel")]
        public IActionResult GetMapel(){
            try{
                // _imailService.Send("TestSub", "jangan sombong");
                _studentService.CreateStudent("angga created");
                var listdata = _dbContext.Mapels.ToList();
                // var teacherList = _dbContext.Teachers.ToList();
                Console.WriteLine(listdata);
                if(listdata == null){
                    return BadRequest(new {Message = "data empty"});
                }
            // var datajoin = from m in listdata join tc in teacherList on m.TeacherId equals tc.Id select new {
            //     Id = m.Id,
            //     namaMapel = m.NamaMapel,
            //     isTugas = m.IsTugas,
            //     teacherName = tc.FullName
            // };
            return Ok(new {Message = "Data list", ListData = listdata});
            }catch(Exception ex){
                // _logger.LogError(ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("test-form")]
        public IActionResult TestForm([FromRoute] int id){
            return Ok(new {Message = $"ini id form route {id}"});
        }


    }
}