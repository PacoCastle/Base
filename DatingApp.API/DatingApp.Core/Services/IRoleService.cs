using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.Core.Models;

namespace DatingApp.Core.Services
{
    public interface IRoleService
    {
        Task<BaseResponse<IEnumerable<String>>> GetRoles();
         Task<BaseResponse<String>> GetRoleByName(String name);
        Task<BaseResponse<Role>>  CreateRole(Role Role);
        /*Task<BaseResponse<Role>>  UpdateRole(Role Role , Role RoleUpdateModel);*/
        
    }
}
