using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetAngularAuthWebApi.Context;
using NetAngularAuthWebApi.Models.Domain;
using OfficeOpenXml;

namespace NetAngularAuthWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExcelController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly string _uploadFolder = "UploadFileExcel";

        public ExcelController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("convert")]
        public IActionResult Convert([FromForm] FileUploadEntity model)
        {
            DataTable table = new DataTable();
            try
            {
                if (model.File != null)
                {
                     // Membuat folder jika belum ada
                     if(Directory.Exists(_uploadFolder)){
                        Directory.CreateDirectory(_uploadFolder);
                     }
                     //create unix name
                     var fileName = $"{DateTime.Now.Ticks.ToString()}-{model.File.FileName}";
                     var filePath = Path.Combine(_uploadFolder, fileName);
                     using(var fileStream = new FileStream(filePath, FileMode.Create)){
                        model.File.CopyTo(fileStream);
                     }
                    using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
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

                                    // Simpan ke database menggunakan DbContext
                                    SaveToDatabase(dr);
                                }
                                // Menghapus file setelah data diunggah dan disimpan
                                // System.IO.File.Delete(filePath);
                                return Ok(new { Message = "Excel Successfully Converted to Data Table and Saved to Database" });
                            }

                        }
                        else
                            return BadRequest(new { Message = "No Work Sheet available in Excel File" });
                    }
                }
                else
                {
                    return BadRequest(new { Message = "File not provided" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"An error occurred: {ex.Message}" });
            }
        }

        private void SaveToDatabase(DataRow dr)
        {
            // Membuat objek ExcelData dari DataRow
            var excelData = new ExcelTam
            {
                No = dr["No"].ToString(),
                NoFrame = dr["NoFrame"].ToString(),
                Dateproduction = dr["Dateproduction"].ToString(),
                Vehicle = dr["Vehicle"].ToString(),
                ConversionType = dr["ConversionType"].ToString(),
                Customer = dr["Customer"].ToString(),
                BodyBuilder = dr["BodyBuilder"].ToString(),
                NoSkrb = dr["NoSkrb"].ToString(),
                Remarks = dr["Remarks"].ToString(),
                Dealer = dr["Dealer"].ToString(),
                Cabang = dr["Cabang"].ToString(),
                Province = dr["Province"].ToString()
                // Tambahkan properti lain sesuai kebutuhan
            };

            // Menyimpan objek ke database menggunakan DbContext
            _dbContext.ExcelTams.Add(excelData);
            _dbContext.SaveChanges();
        }
    }
}