using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace DatingApp.Api.Dtos
{
    public class ConfigurationForRegisterDto
    {
    
        public string ServerName { get; set; }    
        public string Chanel { get; set; }       
        public string Device { get; set; }
        public ICollection<TagForCreationDto> Tags { get; set; }
        
    }
}