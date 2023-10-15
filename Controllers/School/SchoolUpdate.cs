using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetAngularAuthWebApi.Context;
using NetAngularAuthWebApi.Models;
using NetAngularAuthWebApi.Models.Dto;
using Serilog;

namespace NetAngularAuthWebApi.Controllers.School
{
    [ApiController]
    [Route("api/schoolclass-update")]
    public class SchoolUpdate : ControllerBase
    {
        private readonly AppDbContext _schoolContext;
        private readonly ILogger<SchoolUpdate> _logger;

        public SchoolUpdate(AppDbContext schoolContext, ILogger<SchoolUpdate> logger)
        {
            _schoolContext = schoolContext;
            _logger = logger;
        }

        [HttpPatch("edit-class/{id}")]

        public IActionResult Edit(Guid id, [FromBody] SchoolClassDTO schoolClass)
        {
            try
            {
                var isExist = _schoolContext.SchoolClasses.Find(id);
                if (isExist == null)
                {
                    return NotFound();
                }
                //update the data
                isExist.NameOfSchool = schoolClass.NameOfSchool;

                _schoolContext.SchoolClasses.Update(isExist);
                _schoolContext.SaveChanges();

                var updated = new SchoolClass
                {
                    NameOfSchool = schoolClass.NameOfSchool,
                    NameOfHeadSchool = schoolClass.NameOfHeadSchool,
                    ImageSchool = schoolClass.ImageSchool,
                    TimeStart = schoolClass.TimeStart,
                    TimeClose = schoolClass.TimeClose,
                };
                var response = new
                {
                    message = "updated success",
                    data = updated
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Logger = new LoggerConfiguration()
.MinimumLevel.Debug()
.WriteTo.Console()
.WriteTo.File("logs/controller.txt", rollingInterval: RollingInterval.Day).CreateLogger();
                _logger.LogInformation($"SchoolClass {id} is not found");

                return StatusCode(500, ex.Message);
            }
        }
    }
}