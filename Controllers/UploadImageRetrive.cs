using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace NetAngularAuthWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadImageRetrive : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UploadImageRetrive(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("uploadImageRetrive")]

        public async Task<IActionResult> UploadImageRetrivesMulti([FromForm] string productcode, List<IFormFile> _uploadFiles)
        {
            bool Result = false;
            int passcount =0 ; 
            try
            {
                // var _uploadFiles = Request.Form.Files;
                foreach (IFormFile source in _uploadFiles)
                {
                    string filename = source.FileName;
                    string FilePath = GetFilePath(productcode);
                    if (!System.IO.Directory.Exists(FilePath))
                    {
                        System.IO.Directory.CreateDirectory(FilePath);
                    }
                    string imagepath = FilePath + "\\image.png";
                    if (System.IO.File.Exists(imagepath))
                    {
                        System.IO.File.Delete(imagepath);
                    }
                    using (FileStream stream = System.IO.File.Create(imagepath))
                    {
                        await source.CopyToAsync(stream);
                        Result = true;
                    }
                }
            }
            catch (Exception e)
            {

            }
            return Ok(Result);
        }

        [HttpPut("upload-single")]
        public async Task<IActionResult> UploadSingle(IFormFile formFile, string stringcode)
        {
            ApiResponseType response = new ApiResponseType();
            try
            {
                string Filepath = GetFilePath(stringcode);
                // string Filepath = _webHostEnvironment.WebRootPath + "\\Upload\\" + stringcode;
                if (!System.IO.File.Exists(Filepath))
                {
                    System.IO.Directory.CreateDirectory(Filepath);
                }
                string imagepath = Filepath + "\\" + stringcode + ".png";
                if (System.IO.File.Exists(Filepath))
                {
                    System.IO.Directory.Delete(imagepath);
                }
            using (FileStream stream = System.IO.File.Create(imagepath)){
                await formFile.CopyToAsync(stream);
                response.StatusCode = 200;
            }

            }
            catch (Exception ex)
            {

            }
            return Ok(response);
        }

        [NonAction]
        private string GetFilePath(string Filecode)
        {
            return _webHostEnvironment.WebRootPath + "\\Uploads\\" + Filecode;
        }
        [NonAction]

        private string GetFilePathByProducts(string Filecode)
        {
            string ImageUrl = string.Empty;
            string HostUrl = "https://localhost:7023/";
            string filepath = GetFilePath(Filecode);
            string imagepath = Filecode + "\\image.png";
            if (!System.IO.File.Exists(imagepath))
            {
                ImageUrl = HostUrl + "/Uploads/common/noimage.png";
            }
            else
            {
                ImageUrl = HostUrl + "/Uploads/" + Filecode + "/image.png";
            }
            return ImageUrl;
        }
    }
}