using System.Collections.Generic;

namespace DatingApp.Api.Dtos
{
    public class RoleReturn
    {
        public string Name { get; set; }

        public ICollection<MenuReturnDto> Menus { get; set; }
    }
}