using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetAngularAuthWebApi.Context;
using NetAngularAuthWebApi.helpers;
using NetAngularAuthWebApi.Models;
using NetAngularAuthWebApi.Models.Domain;
using NetAngularAuthWebApi.Models.Domain.UploadExcel;
using NetAngularAuthWebApi.Services;
using OfficeOpenXml;

namespace NetAngularAuthWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadExcelController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IExcel _excel; 
        private readonly IConfiguration _configuration;

        public UploadExcelController(AppDbContext context, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }

        [HttpPost("upload-excel")]
        public async Task<IActionResult> UploadExcel(IFormFile file)
        {
            try{
            var connectionString = _configuration.GetConnectionString("DefaultString");
            if (file == null || file.Length == 0)
                return BadRequest("File tidak valid");

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            var employees =  _excel.ReadDataFromExcel(filePath);
            if(employees == null){
                 return BadRequest("File and path tidak valid");
            }
            _excel.BulkInsertToDatabase(employees, connectionString);
            }catch(Exception ex){
                return BadRequest(ex.Message);
            }

            return Ok("Data Excel berhasil diunggah dan disimpan ke database");
        }

         [HttpPost("do-upload-and-save-data")]
        public IActionResult DoUpload(FileUploadEntity model)
        {
            DataTable table = new DataTable();
            string Message = "";
            try
            {
                if (model.File != null)
                {
                    //if you want to read data from a local excel file use this
                    //using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                    using (var stream = model.File.OpenReadStream())
                    {
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        ExcelPackage package = new ExcelPackage();
                        package.Load(stream);
                        if (package.Workbook.Worksheets.Count > 0)
                        {
                            using (ExcelWorksheet workSheet = package.Workbook.Worksheets.First())
                            {
                                int noOfCol = workSheet.Dimension.End.Column;
                                int noOfRow = workSheet.Dimension.End.Row;
                                int rowIndex = 1;
                                for (int c = 1; c <= noOfCol; c++)
                                {
                                    table.Columns.Add(workSheet.Cells[rowIndex, c].Text);
                                }
                                rowIndex = 2;
                                for (int r = rowIndex; r <= noOfRow; r++)
                                {
                                    DataRow dr = table.NewRow();
                                    for (int c = 1; c <= noOfCol; c++)
                                    {
                                        dr[c - 1] = workSheet.Cells[r, c].Value;
                                    }
                                    table.Rows.Add(dr);
                                }
                               Message = "Excel Successfully Converted to Data Table";
                            }
                        }
                        else
                            Message = "No Work Sheet available in Excel File";
                    }
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return Ok(Message);
        }



        // [HttpPost("upload")]
        // [DisableRequestSizeLimit]
        // public async Task<ActionResult> Upload(CancellationToken ct)
        // {
        //     if (Request.Form.Files.Count == 0) return NoContent();
        //     var file = Request.Form.Files[0];
        //     var filePath = SaveFile(file);

        //     //load product request from excel
        //     var productRequest = ExcelHelper.Import<ProductRequest>(filePath);

        //     //save product to database
        //     foreach (var product in productRequest)
        //     {
        //         var products = new Product
        //         {
        //             Id = Guid.NewGuid(),
        //             Name = product.Name,
        //             Quantity = product.Quantity,
        //             Price = product.Price,
        //             IsActive = product.IsActive,
        //             ExpiryDate = product.ExpiryDate
        //         };
        //         await _context.AddAsync(products, ct);
        //     }
        //     await _context.SaveChangesAsync();
        //     return Ok();
        // }
        // private string SaveFile(IFormFile file)
        // {
        //     if (file.Length == 0)
        //     {
        //         throw new BadHttpRequestException("File is empty.");
        //     }
        //     var extension = Path.GetExtension(file.FileName);
        //     var webRootPath = _webHostEnvironment.WebRootPath;
        //     if (string.IsNullOrWhiteSpace(webRootPath))
        //     {
        //         webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        //     }
        //     var folderPath = Path.Combine(webRootPath, "uploads");
        //     if (!Directory.Exists(folderPath))
        //     {
        //         Directory.CreateDirectory(folderPath);
        //     }
        //     var fileName = $"{Guid.NewGuid()}.{extension}";
        //     var filePath = Path.Combine(folderPath, fileName);
        //     using var stream = new FileStream(filePath, FileMode.Create);
        //     file.CopyTo(stream);

        //     return filePath;

        // }


    }
}