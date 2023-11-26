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
        // [HttpPost("upload-files")]
        // public async Task<IActionResult> PostSigleFile([FromBody] FileUploadEntity fileDetails)
        // {
        //     if (fileDetails == null)
        //     {
        //         return BadRequest();
        //     }
        //     try
        //     {
        //         await _uploadService.PostFileAsync(fileDetails.FileDetails, fileDetails.FileType);
        //         return Ok();
        //     }
        //     catch (Exception)
        //     {
        //         throw;
        //     }
        // }
        [HttpGet("download-file")]
        public async Task<IActionResult> DownloadFileAsync(Guid id)
        {
            if (id.ToString() == string.Empty)
            {
                return BadRequest();
            }
            try
            {
                await _uploadService.DownloadFileId(id);
                return Ok();
            }
            catch (Exception)
            {
                throw new ArgumentException();
            }
        }


        [HttpPost]
        public IActionResult MultiUpload(MultipleFilesModel model)
        {
            if (ModelState.IsValid)
            {
                model.IsResponse = true;
                if (model.Files.Count > 0)
                {
                    foreach (var file in model.Files)
                    {

                        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");

                        //create folder if not exist
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);


                        string fileNameWithPath = Path.Combine(path, file.FileName);

                        using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                    }
                    model.IsSuccess = true;
                    model.Message = "Files upload successfully";
                }
                else
                {
                    model.IsSuccess = false;
                    model.Message = "Please select files";
                }
            }
            return null;

        }
    }
}