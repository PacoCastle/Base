using System;

namespace DatingApp.Api.Dtos
{
    public class PartForReturnDto
    {
        public int Id { get; set; }

        public string Name { get; set; }    

        public string Description { get; set; }

        public int Attempts { get; set; }

        public int Status { get; set; }  

        public string NoPlanos { get; set; }    

        public string DiametroTubo { get; set; }    

        public string RPM { get; set; }        
    }
}