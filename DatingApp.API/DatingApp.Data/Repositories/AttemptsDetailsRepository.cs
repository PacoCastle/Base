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
    public class AttemptsDetailsRepository : Repository<AttemptDetail>, IAttemptsDetailsRepository
    {
        private readonly DataContext _context;
        public AttemptsDetailsRepository(DataContext context) 
            : base(context)
        { 
            _context = context;
        }        
    }
}

