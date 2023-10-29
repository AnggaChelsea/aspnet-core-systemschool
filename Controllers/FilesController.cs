using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NetAngularAuthWebApi.Models.Domain;
using NetAngularAuthWebApi.Services;

namespace NetAngularAuthWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly IFileService _uploadService;

        public FilesController(IFileService uploadService)
        {
            _uploadService = uploadService;
        }
        [HttpPost("upload-files")]
        public async Task<IActionResult> PostSigleFile([FromBody] FileUploadModel fileDetails){
            if(fileDetails == null){
                return BadRequest();
            }
            try{
                await _uploadService.PostFileAsync(fileDetails.FileDetails, fileDetails.FileType);
                return Ok();
            }catch(Exception ){
                throw;
            }
        }
        [HttpGet("download-file")]
        public async Task<IActionResult> DownloadFileAsync(Guid id){
            if(id.ToString() == string.Empty){
                return BadRequest();
            }
            try{
                await _uploadService.DownloadFileId(id);
                return Ok();
            }catch(Exception){
                throw new ArgumentException();
            }
        }
    }
}