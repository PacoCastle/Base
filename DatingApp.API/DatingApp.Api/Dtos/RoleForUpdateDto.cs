using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace DatingApp.Api.Dtos
{
    public class RoleForUpdateDto
    {
    
        public ICollection<RoleMenuCreateDto> Menus { get; set; }
        public int status { get; set; }

    }
}