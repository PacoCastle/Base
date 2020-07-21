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
        private readonly UDataContext _context;
        public MachineRepository(UDataContext context) 
            : base(context)
        { 
            _context = context;
        }   
    }
}

