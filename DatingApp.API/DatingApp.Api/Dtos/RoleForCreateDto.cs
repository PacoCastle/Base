using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace DatingApp.Api.Dtos
{
    public class RoleForCreateDto
    {
    
        public string Name { get; set; }
        public ICollection<RoleMenuCreateDto> Menus { get; set; }

    }
}