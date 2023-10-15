using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using NetAngularAuthWebApi.Context;
using NetAngularAuthWebApi.Models.Domain;

namespace NetAngularAuthWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchoolTimeController : ControllerBase
    {
         private readonly IDapperContext _dapperContext;
        public SchoolTimeController(IDapperContext dapperContext){
            _dapperContext = dapperContext;
        }
        [HttpGet("get-time-school")]
        public async Task<List<Branch>> GetAllAsync(){
            var query = "SELECT * FROM " + typeof(Branch).Name;
            using (var connection = _dapperContext.CreateConnection()){
                var result = await connection.QueryAsync<Branch>(query);
                return result.ToList();
            }
        }

        [HttpPost("create-time-school")]
        public async Task Create(Branch branch){
            var query = "INSERT INTO " + typeof(Branch).Name + " (Name, Description, CreatedDate, UpdatedDate) VALUES (@Name, @Description, @CreatedDate, @UpdatedDate)";
            var parameters = new DynamicParameters();
            parameters.Add("Name", branch.Name);
            parameters.Add("Description", branch.Description);
            parameters.Add("CreatedDate", branch.CreatedDate);
            parameters.Add("UpdatedDate", branch.UpdatedDate);

            using (var connection = _dapperContext.CreateConnection()){
                await connection.QueryAsync<Branch>(query, parameters);
            }
        }
    }
}
