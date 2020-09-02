using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace DatingApp.Core.Models
{
    public class Role : IdentityRole<int>
    {
        public int Status { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<RoleMenu> RoleMenus { get; set; }
        [NotMapped]
        public virtual ICollection<Menu> Menus { get; set; }

    }
}