using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.Core.Models;

/*
The main IMachinePartsAttemptsRepository class
Contains all Declarations of Repository Objects that going to be Injected in the constructors  
when a Class implemented it.
*/
/// <summary>
/// The IMachinePartsAttemptsRepository class.
/// Contains all Declarations of Objects that going to be Injected in the constructors  
/// </summary>    
/// 
/// 

namespace DatingApp.Core.Repositories
{
    public interface IMachinePartsAttemptsRepository : IRepository<MachinePartAttempt>
    {
        Task<BaseResponse<MachinePartAttempt>> AddByStored(string MachineModel, string PartModel, string InternalSequence);
        Task<BaseResponse<MachinePartAttempt>> GetByMachinePartSequenceName(string MachineModel, string PartModel, string InternalSequence);
    }
}