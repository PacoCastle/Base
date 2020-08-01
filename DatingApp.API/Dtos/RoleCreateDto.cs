using System.Collections.Generic;

namespace DatingApp.API.Dtos
{
    public class RoleCreateDto
    {
        public string Name { get; set; }

        public ICollection<MenuReturnDto> Menus { get; set; }
    }
}