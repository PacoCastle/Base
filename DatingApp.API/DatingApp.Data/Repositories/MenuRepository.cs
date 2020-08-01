using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DatingApp.Core.Models;
using DatingApp.Core.Repositories;
using Microsoft.Data.SqlClient;
using System;
using Microsoft.AspNetCore.Identity;

namespace DatingApp.Data.Repositories
{
    public class MenuRepository : Repository<Menu>, IMenuRepository
    {
        private readonly DataContext _context;    
         
        public MenuRepository(DataContext context) 
            : base(context)
        { 
            _context = context;            
        }       
         
    }
}

