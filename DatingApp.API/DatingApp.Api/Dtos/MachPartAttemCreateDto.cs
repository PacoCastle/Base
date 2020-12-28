
using System;
using Microsoft.AspNetCore.Http;

namespace DatingApp.Api.Dtos
{
    public class MachPartAttemCreateDto
    {
    
        public string MachineModel { get; set; }

        public string PartModel { get; set; }

        public string InternalSequence { get; set; }
        
    }
}