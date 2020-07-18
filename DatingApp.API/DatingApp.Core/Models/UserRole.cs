using Microsoft.AspNetCore.Identity;

namespace DatingApp.Core
{
    public class UserRole : IdentityUserRole<int>
    {
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}