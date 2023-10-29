using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetAngularAuthWebApi.Models.Domain;
using NetAngularAuthWebApi.Models.Dto;

namespace NetAngularAuthWebApi.Services
{
    public interface IFileService
    {
        public Task DownloadFileId(Guid fileName);
        public Task PostFileAsync(IFormFile fileData, FileType fileType);
        // public Task PostMultiAsync(List<FileUploadModel> fileData);
        public Task PostSingleFileAsync(IFormFile fileData, FileType fileType);
    }
}