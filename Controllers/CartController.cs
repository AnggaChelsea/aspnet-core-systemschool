using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetAngularAuthWebApi.Context;
using NetAngularAuthWebApi.Models.Domain;
using NetAngularAuthWebApi.Models.Dto;

namespace NetAngularAuthWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly AppDbContext _cartCourse;
        public CartController(AppDbContext cartCourse)
        {
            _cartCourse = cartCourse;
        }

        [HttpPost("add-cart")]
        public async Task<IActionResult> AddToChart([FromBody] CartCourseDto cartCourseDto){
            if(cartCourseDto == null)
            return BadRequest();

            var body = new CartCourse{
                CourseId = cartCourseDto.CourseId,
                StudentId = cartCourseDto.StudentId,
                Quantity = cartCourseDto.Quantity,
                Total = cartCourseDto.Total,
            };
            if(body == null)
            return BadRequest();

           var resBody = await _cartCourse.CartCourses.AddAsync(body);
           await  _cartCourse.SaveChangesAsync();

            return Ok(new {Message = "success add cart", body = resBody});
        }
    }
}