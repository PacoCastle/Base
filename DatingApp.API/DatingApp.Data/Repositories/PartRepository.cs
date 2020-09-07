using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DatingApp.Core.Models;
using DatingApp.Core.Repositories;
using Microsoft.Data.SqlClient;
using System;

namespace DatingApp.Data.Repositories
{
    public class PartRepository : Repository<PartModel>, IPartRepository
    {
        private readonly DataContext _context;
        public PartRepository(DataContext context) 
            : base(context)
        { 
            _context = context;
        }        
        public Task<PartModel> GetPartByName(string name)
        {
            return _context.PartModel                
                .SingleOrDefaultAsync(p => p.Name == name);
        }        
    }
}

