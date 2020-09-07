using System.Collections.Generic;

namespace DatingApp.API.Dtos
{
    public class RoleUpdateDto
    {
        public string Name { get; set; }

        public ICollection<MenuReturnDto> Menus { get; set; }
    }
}