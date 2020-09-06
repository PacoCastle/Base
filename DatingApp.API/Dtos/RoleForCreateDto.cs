using System;
using System.Collections.Generic;
using DatingApp.API.Migrations;
using Microsoft.AspNetCore.Http;

namespace DatingApp.API.Dtos
{
    public class RoleForCreateDto
    {
    
        public string Name { get; set; }
        public ICollection<RoleMenuCreateDto> Menus { get; set; }

    }
}