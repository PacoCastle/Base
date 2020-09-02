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
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly DataContext _context;
        public RoleRepository(RoleManager<Role> roleManager, DataContext context )
                : base(context)
        { 
            _roleManager = roleManager;
            _context = context;
        }        
        public async Task<IEnumerable<String>> GetRoles()
        {
            //List<string> roles = roleMngr.Roles.Select(x => x.Name).ToList();
            var roleMngr = await _roleManager.Roles
            .Select(r => r.Name)
            .ToListAsync();
            return roleMngr; 
        }  

        public async Task<string> GetRoleByName(string name)
        {
            var roleMngr = await _roleManager.Roles.AsNoTracking().Where(w =>
                 w.Name == name )
            .Select(r => r.Name)
            .FirstOrDefaultAsync();
            return roleMngr; 
        }   
        public async Task<Role> CreateRole(Role role)
        {
            _roleManager.CreateAsync(role).Wait();
            return role; 
        } 

             
    }
}

