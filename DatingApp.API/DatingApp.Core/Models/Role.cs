using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DatingApp.Core
{
    public class Role : IdentityRole<int>
    {
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}