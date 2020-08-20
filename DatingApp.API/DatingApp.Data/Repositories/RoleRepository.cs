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
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<Role> _roleManager;
        public RoleRepository(RoleManager<Role> roleManager)
        { 
            _roleManager = roleManager;
        }        
        public async Task<IEnumerable<String>> GetRoles()
        {
            //List<string> roles = roleMngr.Roles.Select(x => x.Name).ToList();
            var roleMngr = await _roleManager.Roles
            .Select(r => r.Name)
            .ToListAsync();
            return roleMngr; 
        }  

        public async Task<String> GetRoleByName(String name)
        {
            var roleMngr = await _roleManager.Roles.Where(w =>
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

