using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace NetAngularAuthWebApi.Context
{
    public interface IDapperContext
    {
        IDbConnection CreateConnection();
    }



}