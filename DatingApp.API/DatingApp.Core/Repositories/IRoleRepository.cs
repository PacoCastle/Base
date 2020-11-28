using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.Core.Models;

namespace DatingApp.Core.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<IEnumerable<Role>> GetRoles();
        Task<Role> GetRoleById(int id);
        Task<Role> CreateRole(Role role);
        
        Task<string> GetRoleByName(String name);
    }
}