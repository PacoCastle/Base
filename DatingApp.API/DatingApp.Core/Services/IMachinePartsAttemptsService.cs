using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.Core.Models;

namespace DatingApp.Core.Services
{
    public interface IMachinePartsAttemptsService
    {
        Task<MachinePartAttempt> GetMachinePartAttempt(int id);
        Task<IEnumerable<MachinePartAttempt>> GetMachinePartsAttempts();  
        Task<int> AddByStored(string MachineModel, string PartModel, string InternalSequence);

        Task UpdateMachinePartAttempt(MachinePartAttempt MachPartAttemToBeUpdate, MachinePartAttempt machinePartAttempt);
    }
}
