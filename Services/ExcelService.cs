using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using NetAngularAuthWebApi.Models.Domain;
using OfficeOpenXml;

namespace NetAngularAuthWebApi.Services
{
    public class ExcelService : IExcel
    {
        private readonly IConfiguration _configuration;

        public ExcelService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

       

        public List<Employee> ReadDataFromExcel(string filePath)
        {
            var employees = new List<Employee>();
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0]; // Assuming the data is in the first worksheet

                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {
                    var employee = new Employee
                    {
                        EmployeeId = Convert.ToInt32(worksheet.Cells[row, 1].Value),
                        FirstName = worksheet.Cells[row, 2].Value?.ToString(), // Use null-conditional operator to handle null values
                        LastName = worksheet.Cells[row, 3].Value?.ToString(),  // Use null-conditional operator to handle null values
                                                                               // Map other properties accordingly
                    };

                    employees.Add(employee);
                }
            }

            return employees;
        }

         public void BulkInsertToDatabase(List<Employee> employees, string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create a DataTable to hold your data
                DataTable dataTable = new DataTable("Employee");
                dataTable.Columns.Add("EmployeeId", typeof(int));
                dataTable.Columns.Add("FirstName", typeof(string));
                dataTable.Columns.Add("LastName", typeof(string));
                // Add other columns as needed

                // Fill the DataTable with data from Excel
                foreach (var employee in employees)
                {
                    dataTable.Rows.Add(employee.EmployeeId, employee.FirstName, employee.LastName /* Add other values */);
                }

                // Create the SqlBulkCopy object
                using (var bulkCopy = new SqlBulkCopy(connection))
                {
                    bulkCopy.DestinationTableName = "Employee"; // Replace with your actual table name
                    bulkCopy.BatchSize = 1000; // Adjust the batch size as needed

                    // Optionally, map Excel columns to SQL table columns if they don't match exactly
                    bulkCopy.ColumnMappings.Add("EmployeeId", "EmployeeId");
                    bulkCopy.ColumnMappings.Add("FirstName", "FirstName");
                    bulkCopy.ColumnMappings.Add("LastName", "LastName");
                    // Add mappings for other columns if needed

                    // Perform the bulk copy
                    bulkCopy.WriteToServer(dataTable);
                }
            }
        }


    }
}