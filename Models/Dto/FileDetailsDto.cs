using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

namespace NetAngularAuthWebApi.Models.Dto
{
    public class FileDetailsDto
    {
         [Key]
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public byte[] FileData { get; set; }
        public FileType fileType { get; set; }
    }
}