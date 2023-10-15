using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetAngularAuthWebApi.Context;
using NetAngularAuthWebApi.Models.Domain;
using NetAngularAuthWebApi.Models.Dto;

namespace NetAngularAuthWebApi.Controllers
{
    [Authorize(Roles = "admin")]
    [ApiController]
    [Route("api/jadwal")]
    public class JadwalController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public JadwalController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("create-jadwal")]
        public async Task<IActionResult> CreateJadwalAsync([FromBody] JadwalDtos jadwalDtos){
            try{
                if(jadwalDtos == null){
                    return BadRequest();
                }
                var body = new Jadwal {
                    JamMapel = jadwalDtos.JamMapel,
                    KelasId = jadwalDtos.KelasId,
                    MapelId = jadwalDtos.MapelId
                };
                var dataListCheck = _dbContext.Jadwals.FirstOrDefaultAsync(e => e.JamMapel == jadwalDtos.JamMapel);
                if(dataListCheck is not null){
                    return BadRequest(new {Message = "Please select other time"});
                }
                await _dbContext.Jadwals.AddAsync(body);
                await _dbContext.SaveChangesAsync();
                return Ok(new {Message = "Success created", data = body});
            }catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-jadwal")]
        [AllowAnonymous]
        public IActionResult GetJadwal(){
            try{
                var listJadwal = _dbContext.Jadwals.ToList();
                if(listJadwal.Count < 0 || listJadwal is null){
                    return NotFound(new {Message = "data empty"});
                }
                return Ok(new {Message = "success", Data = listJadwal});
            }catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }
    }
}