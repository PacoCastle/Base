
using System;
using Microsoft.AspNetCore.Http;

namespace DatingApp.API.Dtos
{
    public class MachPartAttemRegisterDto
    {
    
        public string MachineModel { get; set; }

        public string PartModel { get; set; }

        public string InternalSequence { get; set; }
        
    }
}