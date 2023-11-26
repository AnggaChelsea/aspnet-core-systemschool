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

                                    // Simpan ke database menggunakan DbContext
                                    SaveToDatabase(dr);
                                }

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
            var excelData = new ExcelData
            {
                Column1 = dr["Column1"].ToString(),
                Column2 = dr["Column2"].ToString(),
                // Tambahkan properti lain sesuai kebutuhan
            };

            // Menyimpan objek ke database menggunakan DbContext
            _dbContext.ExcelData.Add(excelData);
            _dbContext.SaveChanges();
        }
    }
}