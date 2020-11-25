
using System;
using Microsoft.AspNetCore.Http;

namespace DatingApp.Api.Dtos
{
    public class TagForCreationDto
    {
        public string TagName { get; set; }    
        public string IsStart { get; set; }    
        public string IsActive { get; set; }  
        public string ColumnDataType {get;set;}
        public string ColumnNullable {get;set;}
    }
}