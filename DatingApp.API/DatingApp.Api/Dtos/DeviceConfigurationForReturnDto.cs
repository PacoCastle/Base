using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace DatingApp.Api.Dtos
{
    public class DeviceConfigurationForReturnDto
    {
    
        public int Id { get; set; }
        public string ServerName { get; set; }    
        public string Chanel { get; set; }       
        public string Device { get; set; }
        public string TagName { get; set; }    
        public string IsStart { get; set; }    
        public string IsActive { get; set; }  
        public string ColumnDataType {get;set;}
        public string ColumnNullable {get;set;}
        
    }
}