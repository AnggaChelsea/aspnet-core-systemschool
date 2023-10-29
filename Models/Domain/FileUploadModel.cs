using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using NetAngularAuthWebApi.Models.Dto;

namespace NetAngularAuthWebApi.Models.Domain
{
    public class FileUploadModel
    {
        public Guid Id { get; set; }
        public IFormFile FileDetails { get; set; }
        public FileType FileType { get; set; }
    }
}