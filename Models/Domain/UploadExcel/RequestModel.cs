using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetAngularAuthWebApi.Models.Domain.UploadExcel
{
    public class ProductRequest
    {

        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public bool IsActive { get; set; }
        public string ExpiryDate { get; set; }
    }

}