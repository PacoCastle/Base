using System;
using Microsoft.AspNetCore.Http;

namespace DatingApp.API.Dtos
{
    public class PartForRegisterDto
    {
    
        public string Name { get; set; }    

        public string Description { get; set; }

        public int Attempts { get; set; }

        public int Status { get; set; } 


        public string NoPlanos { get; set; }    

        public string DiametroTubo { get; set; }    

        public string RPM { get; set; }    
    }
}