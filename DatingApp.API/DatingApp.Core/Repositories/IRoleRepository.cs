using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.Core.Models;

namespace DatingApp.Core.Repositories
{
    public interface IRoleRepository
    {
        Task<IEnumerable<String>> GetRoles();
        Task<String> GetRoleByName(String name);
        Task<Role> CreateRole(Role role);
    }
}