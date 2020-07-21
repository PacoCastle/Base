using System;

namespace DatingApp.API.Dtos
{
    public class AttemptDetailReturnDto
    {
        public int Id { get; set; }

        public int MachinePartAttemptId { get; set; } 

        public string AnguloLH { get; set; }

        public string MasaLH { get; set; }

        public string AnguloCL { get; set; }

        public string MasaCL { get; set; }

        public string AnguloRH { get; set; }

        public string MasaRH { get; set; }

        public bool IsAccepted { get; set; }

        public int Attempt { get; set; }     
    }
}