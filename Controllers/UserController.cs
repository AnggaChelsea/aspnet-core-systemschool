using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using BCrypt.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NetAngularAuthWebApi.Configuration;
using NetAngularAuthWebApi.Context;
using NetAngularAuthWebApi.Models;
using NetAngularAuthWebApi.Models.Dto;

namespace NetAngularAuthWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //inject dbcontec
        private readonly IMapper _mapper;
        private readonly AppDbContext _authContext;
        public UserController(AppDbContext authContext)
        {
            _authContext = authContext;
        }

        //method post route authenticate
        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] Student userObject)
        {
            if (userObject == null)
                return BadRequest();

            var reqbody = new StudentDtoLogin {
                FullName = userObject.FullName,
                NIM = userObject.NIM,
            };

            var userListStudent = _authContext.Students.FirstOrDefault();

            var user = await _authContext.Students.FirstOrDefaultAsync(x => x.FullName == reqbody.FullName && x.NIM == reqbody.NIM);
            if (user == null)
                return NotFound(new { Message = "User Not Found" });

            var response = new {
            Message = "Success Login",
            Data = reqbody
          };

            return Ok(response);

        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] User userObj)
        {
            if (userObj == null)
                return BadRequest();

            if (_authContext.Users.Any(x => x.UserName == userObj.UserName))
                return BadRequest(new { Message = "username '" + userObj.UserName + "' is already exist" });

            var new_user = new User() {
                Name = userObj.Name,
                UserName = userObj.UserName,
                Email = userObj.Email,
                Password = userObj.Password,
                Role = userObj.Role,
            };

          await _authContext.Users.AddAsync(userObj);
          await _authContext.SaveChangesAsync();
          
          return Ok(new {Message = "Success regis"});
            
        }
        //get user
        [HttpGet("getUserList")]
        public IActionResult GetListUser()
        {
            var userList = _authContext.Users.ToList();
            if (userList == null || userList.Count == 0)
                return NotFound();


            return Ok(userList);

        }
    }


}

