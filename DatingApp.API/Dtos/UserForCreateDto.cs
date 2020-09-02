using System;

namespace DatingApp.API.Dtos
{
    public class UserForCreateDto
    {        
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }        
        public DateTime DateOfBirth { get; set; }        
        public DateTime Created { get; set; }
        public UserForCreateDto()
        {
            Created = DateTime.Now;
        }
        public string[] Roles { get; set; }
    }
}