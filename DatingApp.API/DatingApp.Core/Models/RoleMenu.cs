using System;

namespace DatingApp.Core.Models
{

    //
    // Summary:
    //     Represents the link between a user and a role.
    //
    // Type parameters:
    //   TKey:
    //     The type of the primary key used for users and roles.
    // public class IdentityRoleMenu<TKey> where TKey : IEquatable<TKey>
    // {
    //     //
    //     // Summary:
    //     //     Gets or sets the primary key of the role that is linked to the user.
    //     public virtual TKey RoleId { get; set; }
    //     //
    //     // Summary:
    //     //     Gets or sets the primary key of the user that is linked to a role.
    //     public virtual TKey MenuId { get; set; }
    // }
    public class RoleMenu //: IdentityRoleMenu <int>
    { 

        public virtual Role Role { get; set; }

        public virtual Menu Menu { get; set; }

    
    }
}