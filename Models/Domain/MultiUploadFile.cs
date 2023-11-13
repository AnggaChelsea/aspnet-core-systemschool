using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetAngularAuthWebApi.Models.Domain
{
    public class MultiUploadFile
    {

        public int Id { get; set; }
        public string FileName { get; set; }
        public byte[] Content { get; set; }
    }

    public class FileViewModel
    {
        public List<IFormFile> FormFile { get; set; }
    }
}