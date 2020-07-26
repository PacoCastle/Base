using System;

/*
The main MachinePartAttempt class
Contains all properties to use in Context for work with Data Base
*/
/// <summary>
/// The AutoMapperProfiles class.
/// Contains all properties to use in Context for work with Data Base
/// </summary>    
/// 
/// 
namespace DatingApp.Core.Models
{
    public class MachinePartAttempt
    {
        public int Id { get; set; }

        public int MachineModelId { get; set; }

        public int PartModelId { get; set; }

        public string InternalSequence { get; set; }

        public int DefaultAttempts { get; set; }
        public int AvailableAttempts { get; set; }        
          
    }
}

