using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace NetAngularAuthWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        [HttpPost("file-upload")]
        public async Task<IActionResult> UploadFile(IFormFile file, CancellationToken cancellationToken)
        {
            var result = await UploadFileAsync(file);
            return Ok(result);
        }
        private async Task<string> UploadFileAsync(IFormFile file)
        {
            string kode = "register-siup";
            string filename = "";
            try
            {
                
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                filename = kode + DateTime.Now.Ticks.ToString() + extension;

                var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Upload");
                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }

                var exactPath = Path.Combine(uploadDirectory, filename);
                using (var stream = new FileStream(exactPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
                // Handle the exception, log, or return an error message as needed
            }
            return filename;
        }



        [HttpGet("download-file")]
        public async Task<IActionResult> DownloadFile(string filename)
        {
            try
            {
                var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Upload"); // Provide the correct directory path
                var filePath = Path.Combine(uploadDirectory, filename);

                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(filePath, out var contentType))
                {
                    contentType = "application/octet-stream";
                }

                var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
                return File(bytes, contentType, Path.GetFileName(filePath));
            }
            catch (Exception ex)
            {
                // Handle the exception, log, or return an error response as needed
                return BadRequest("Failed to download the file: " + ex.Message);
            }
        }

    }
}