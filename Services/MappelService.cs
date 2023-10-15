using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetAngularAuthWebApi.Context;
using NetAngularAuthWebApi.Models.Domain;

namespace NetAngularAuthWebApi.Services
{
    public class MappelService : IMappelService
    {
        private readonly AppDbContext _appDbContext;
        public MappelService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;    
        }

        IEnumerable<Mapel> IMappelService.GetMapels()
        {
            throw new NotImplementedException();
        }
    }
}