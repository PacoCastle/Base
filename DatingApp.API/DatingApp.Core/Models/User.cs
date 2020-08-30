using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace DatingApp.Core.Models
{
    public class User : IdentityUser<int>
    {

        public string Name { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }        
        
        // public virtual ICollection<Photo> Photos { get; set; }
        // public virtual ICollection<Like> Likers { get; set; }
        // public virtual ICollection<Like> Likees { get; set; }
        // public virtual ICollection<Message> MessagesSent { get; set; }
        // public virtual ICollection<Message> MessagesReceived { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
         [NotMapped]        
         public virtual ICollection<String> RoleNames { get; set; }

        [NotMapped]
        public String token { get; set; }

        //public virtual ICollection<RoleMenu> RoleMenu { get; set; }
    }
}