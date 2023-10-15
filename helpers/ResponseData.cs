using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetAngularAuthWebApi.Models;

namespace NetAngularAuthWebApi.helpers
{
    public class ResponseData
    {
        public string Message { get; set; }
        public List<SchoolClass> Data { get; set; } 
        
    }
}