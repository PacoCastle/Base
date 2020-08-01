namespace DatingApp.API.Dtos
{
    public class UserForUpdateDto
    {
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }       
        public string[] RoleNames { get; set; }
    }
}