using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.Core.Models;

/*
The main IMachinePartsAttemptsService class
Contains all Declarations of Service Objects that going to be Injected in the constructors  
when a Class implemented it.
*/
/// <summary>
/// The IMachinePartsAttemptsService class.
/// Contains all Declarations of Objects that going to be Injected in the constructors  
/// </summary>    
/// 
/// 

namespace DatingApp.Core.Services
{
    public interface IMachinePartsAttemptsService
    {
        Task<BaseResponse<MachinePartAttempt>> GetMachinePartAttempt(int id);
        Task<BaseResponse<IEnumerable<MachinePartAttempt>>> GetMachinePartsAttempts();  
        Task<BaseResponse<MachinePartAttempt>> AddByStored(string MachineModel, string PartModel, string InternalSequence);
        Task<BaseResponse<MachinePartAttempt>> UpdateMachinePartAttempt(MachinePartAttempt MachPartAttemToBeUpdate, MachinePartAttempt MachinePartAttemptForUpdate);
    }
}
