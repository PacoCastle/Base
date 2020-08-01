using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DatingApp.Core.Models
{
    public class Role : IdentityRole<int>
    {
        //public string Name { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }

        //public virtual ICollection<RoleMenu> RoleMenus { get; set; }
    }
}