using System;
using System.Collections.Generic;
using DatingApp.API.Migrations;
using Microsoft.AspNetCore.Http;

namespace DatingApp.API.Dtos
{
    public class RoleForUpdateDto
    {
    
        public ICollection<RoleMenuCreateDto> Menus { get; set; }

    }
}