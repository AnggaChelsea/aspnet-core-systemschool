using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetAngularAuthWebApi.Context;
using NetAngularAuthWebApi.Models.Domain;
using NetAngularAuthWebApi.Models.Dto;

namespace NetAngularAuthWebApi.Controllers
{
    [Authorize(Roles = "admin")]
    [ApiController]
    [Route("api/roles")]
    public class RolesController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public RolesController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("create-roles")]
        // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRoleAsync([FromBody] RolesDto roles)
        {
            try
            {
                if (roles == null)
                {
                    return BadRequest(new { Message = "Dont be null" });
                }
                var body = new Roles
                {
                    Name = roles.Name,
                };
                if (body == null)
                {
                    return BadRequest();
                }
                var dataRolesIsExist = _dbContext.Roles.FirstOrDefault(x => x.Name == body.Name);
                if (dataRolesIsExist is not null)
                {
                    return BadRequest(new { Message = "Name of roles already exist" });
                }
                await _dbContext.Roles.AddAsync(body);
                await _dbContext.SaveChangesAsync();
                return Ok(new { Message = "Created data", Data = body });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-roles")]
        public IActionResult GetRolesAsync()
        {
            try
            {
                var rolesData = _dbContext.Roles.ToList();
                if (rolesData == null)
                {
                    return BadRequest();
                }
                return Ok(new { Message = "Data Get", Data = rolesData });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-roles/{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteRoles(Guid id)
        {
            try
            {
                var dataRoles = _dbContext.Roles.FirstOrDefault(x => x.Id == id);
                if (dataRoles == null)
                {
                    return BadRequest(new { Message = "Data Not Found" });
                }
                return Ok(new {Message = "Deleted success"});
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
             }

        }

        [HttpGet("get-details/{id}")]
        public async Task<IActionResult> GetDetails(Guid id){
            if(id == Guid.Empty){
                return NotFound();
            }

            var list = _dbContext.Roles.Find(id);
            if(list == null){
                return NotFound(new {Message = "database not found"});
            }

            return Ok(list);
        }

    }
}