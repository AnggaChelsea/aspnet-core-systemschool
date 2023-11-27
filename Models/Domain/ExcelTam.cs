using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetAngularAuthWebApi.Models.Domain
{
    public class ExcelTam
    {
        public Guid Id { get; set; }
        public string? No { get; set; }
        public string NoFrame { get; set; } 
        public string Dateproduction { get; set; }
        public string Vehicle { get; set; }
        public string ConversionType { get; set; }
        public string Customer  { get; set; }
        public string BodyBuilder { get; set; }
        public string NoSkrb { get; set; }
        public string Remarks { get; set; }
        public string Dealer { get; set; }
        public string Cabang { get; set; }
        public string Province { get; set; }
    

    }
}