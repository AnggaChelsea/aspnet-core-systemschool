using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetAngularAuthWebApi.Models.Domain;

namespace NetAngularAuthWebApi.Services
{
    public interface IExcel
    {
        List<Employee> ReadDataFromExcel(string filePath);
        void BulkInsertToDatabase(List<Employee> employees, string connectionString);
    }
}