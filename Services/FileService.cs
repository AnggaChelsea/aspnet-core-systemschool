
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetAngularAuthWebApi.Context;
using NetAngularAuthWebApi.Models.Domain;
using NetAngularAuthWebApi.Models.Dto;

namespace NetAngularAuthWebApi.Services
{
    public class FileService : IFileService
    {
        private readonly AppDbContext _appDbContext;

        public FileService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task DownloadFileId(Guid Id)
        {
            try{
                var file = await _appDbContext.FileDetails.Where(x => x.Id == Id).FirstOrDefaultAsync();
                var content = new System.IO.MemoryStream(file.FileData);
                var path = Path.Combine(
                    Directory.GetCurrentDirectory(), "FileDownload",
                    file.FileName
                );
            }catch(Exception){
            throw new NotImplementedException();
            }
        }

        public async Task PostFileAsync(IFormFile fileData, FileType fileType)
        {
           throw new NotImplementedException();

//             try{
//                 var fileDetails = new FileDetails(){
//                     Id = new Guid(),
//                     FileName = fileData.FileName,
//                     FieldType = fileType
//                 };
//                 using (var stream = new MemoryStream()){
//                     fileData.CopyTo(stream);
//                     fileDetails.FileData = stream.ToArray();
//                 };
//                 var result = _appDbContext.FileDetails.Add(fileDetails);
//                 await _appDbContext.SaveChangesAsync();
// ;            }catch{
//                     throw new NotImplementedException();
//             }
        }

        public async Task PostSingleFileAsync(IFormFile fileData, FileType fileType)
        {
            try{
               var fileDetails = new FileDetails() {
                Id = new Guid(),
                FileName = fileData.FileName,
                FileType = fileType
               };
               using (var stream = new MemoryStream()){
                fileData.CopyTo(stream);
                fileDetails.FileData = stream.ToArray();
               }
               var result = _appDbContext.FileDetails.Add(fileDetails);
               await _appDbContext.SaveChangesAsync();
            }catch(Exception){
            throw new NotImplementedException();
            }
        }
    }
}