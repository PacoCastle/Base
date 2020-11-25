using System;
using Microsoft.AspNetCore.Http;

namespace DatingApp.Api.Dtos
{
    public class MachineRegisterDto
    {
    
        public string Name { get; set; }    

        public string Description { get; set; }
        
        public int Status { get; set; }     

    }
}