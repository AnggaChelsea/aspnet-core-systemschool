using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using NetAngularAuthWebApi.Configuration;
using NetAngularAuthWebApi.Context;
using NetAngularAuthWebApi.Models;
using NetAngularAuthWebApi.Models.Dto;
using NetAngularAuthWebApi.Services;

using ITfoxtec.Identity.Saml2;
using ITfoxtec.Identity.Saml2.Schemas;
using ITfoxtec.Identity.Saml2.MvcCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Authentication;
using NetAngularAuthWebApi.Config;

namespace NetAngularAuthWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly AppDbContext _appdbContext;
        private readonly IConfiguration _config;
        private readonly AppDbContext _student;
        private readonly IMailService _mailService;
        private readonly ILogger<AuthenticationController> _logger;

        const string relayStateReturnUrl = "ReturnUrl";
        private readonly Saml2Configuration _configsso;




        // private readonly JwtConfig _jwtConfig;


        public AuthenticationController(AppDbContext appdbContext, IConfiguration config, AppDbContext student, ILogger<AuthenticationController> logger, IMailService mailService, IOptions<Saml2Configuration> configsso)
        {
            _appdbContext = appdbContext;
            _config = config;
            _student = student;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService;
            _configsso = configsso.Value;
        }


        // [Route("Login-sso")]
        
        // public IActionResult Login(string returnUrl = null)
        // {
        //     var binding = new Saml2RedirectBinding();
        //     binding.SetRelayStateQuery(new Dictionary<string, string> { { relayStateReturnUrl, returnUrl ?? Url.Content("~/") } });

        //     return binding.Bind(new Saml2AuthnRequest(_configsso)).ToActionResult();
        // }


        // [Route("AssertionConsumerService")]
        // public async Task<IActionResult> AssertionConsumerService()
        // {
        //     var binding = new Saml2PostBinding();
        //     var saml2AuthnResponse = new Saml2AuthnResponse(_configsso);

        //     binding.ReadSamlResponse(Request.ToGenericHttpRequest(), saml2AuthnResponse);
        //     if (saml2AuthnResponse.Status != Saml2StatusCodes.Success)
        //     {
        //         throw new AuthenticationException($"SAML Response status: {saml2AuthnResponse.Status}");
        //     }
        //     binding.Unbind(Request.ToGenericHttpRequest(), saml2AuthnResponse);
        //     await saml2AuthnResponse.CreateSession(HttpContext, claimsTransform: (claimsPrincipal) => ClaimsTransform.Transform(claimsPrincipal));

        //     var relayStateQuery = binding.GetRelayStateQuery();
        //     var returnUrl = relayStateQuery.ContainsKey(relayStateReturnUrl) ? relayStateQuery[relayStateReturnUrl] : Url.Content("~/");
        //     return Redirect(returnUrl);
        // }



        [HttpPost("register-student")]
        public async Task<IActionResult> CreateStudent([FromBody] StudentDTO studentdBody)
        {
            Random rnd = new Random();
            int nim = rnd.Next(1, 10000);
            if (studentdBody == null)
                return BadRequest(new { Message = "Input data" });

            var bodyStudent = new Student
            {
                FullName = studentdBody.FullName,
                Age = studentdBody.Age,
                Address = studentdBody.Address,
                IsChangeSchool = false,
                NameSchoolBefore = "false",
                Image = studentdBody.Image,
                NIM = nim,
                RolesId = studentdBody.RolesId
            };
            if (bodyStudent == null)
            {
                return BadRequest("Please provide a property");
            }
            await _appdbContext.Students.AddAsync(bodyStudent);
            await _appdbContext.SaveChangesAsync();
            var response = new
            {
                Message = "Success Created",
                data = studentdBody,
            };
            return Ok(response);
        }


        [HttpPost("student-login")]
        public async Task<IActionResult> StudentLogin([FromBody] StudentDtoLogin studentDtoLogin)
        {


            var client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("a989fe06692cba", "51b7bae4ee18fe"),
                EnableSsl = true
            };
            client.Send("adeadeaja2121@gmail.com", "muhidabdul168@gmail.com", "Hello world", "testbody");
            Console.WriteLine("Sent");


            if (studentDtoLogin == null)
            {
                _logger.LogInformation("jangan di kosongnkan");

                return BadRequest();
            }
            var responseNotFound = new
            {
                Code = 404,
                Message = $"{studentDtoLogin.FullName} Not Found",
            };
            var existing_user = await _student.Students.FirstOrDefaultAsync(x => x.FullName == studentDtoLogin.FullName && x.NIM == studentDtoLogin.NIM);
            if (existing_user is null)
            {
                _logger.LogInformation($"Student email {studentDtoLogin.FullName} not found");
                return NotFound(new { Message = responseNotFound });
            }
            var nameRoles = _appdbContext.Roles.FirstOrDefault(e => e.Id == existing_user.RolesId);
            Console.WriteLine(nameRoles.Name);
            var jwtToken = GenerateToken(existing_user, nameRoles.Name);
            var responseBody = new
            {
                Message = "Success Get Login",
                Data = existing_user
            };
            // }
            return Ok(new
            {
                Message = "success",
                Token = jwtToken,
            });
        }

        private string DoGenerateToken(Student student)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var configuration = Encoding.ASCII.GetBytes(_config.GetSection("JwtSettings:key").Value);
            var encodingToken = configuration;
            var insurane = _config.GetSection("JwtSettings:Issuer").Value;
            var Audience = _config.GetSection("JwtSettings:Audience").Value;

            var securityTokenDesciptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddMinutes(10),
                Issuer = insurane,
                Audience = Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(encodingToken), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(securityTokenDesciptor);
            return tokenHandler.WriteToken(token);
        }
        private string GenerateToken(Student student, string typeRole)
        {
            List<Claim> claims = new List<Claim>{
                new Claim(ClaimTypes.Name, student.Id.ToString()),
                new Claim(ClaimTypes.Role, typeRole),
                new Claim(ClaimTypes.Name, student.FullName),
                new Claim(ClaimTypes.Name, student.FullName),
                new Claim(ClaimTypes.Name, student.Age.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("J1LK223PKYD7HXXD5Q7V9AKS5ZTLSY-J1LK223PKYD7HXXD5Q7V9AKS5ZTLSY"));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;

        }
    }
}