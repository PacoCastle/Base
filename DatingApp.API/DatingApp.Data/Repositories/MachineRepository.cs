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
    public class MachineRepository : Repository<MachineModel>, IMachineRepository
    {
        private readonly DataContext _context;
        public MachineRepository(DataContext context) 
            : base(context)
        { 
            _context = context;
        }  
        public Task<MachineModel> GetMachineByName(string name)
        {
            return _context.MachineModel                
                .SingleOrDefaultAsync(p => p.Name == name);
        }    
    }
}

