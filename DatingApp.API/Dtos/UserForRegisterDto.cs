using System;

namespace DatingApp.API.Dtos
{
    public class UserForRegisterDto
    {        
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }        
        public DateTime DateOfBirth { get; set; }        
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public UserForRegisterDto()
        {
            Created = DateTime.Now;
            LastActive = DateTime.Now;
        }
        public string[] RoleNames { get; set; }
    }
}