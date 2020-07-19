using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.Core.Models;

namespace DatingApp.API.Services
{
    public class MachinePartsAttemptsService_ : IMachinePartsAttemptsService_
    {
        private readonly IDatingRepository _repo;
        public MachinePartsAttemptsService_(IDatingRepository repo)
        {
            _repo = repo;    
        }

        public async Task<MachinePartAttempt> GetMachinePartAttempt(int id)
        {
            return await _repo.GetMachinePartAttempt(id);
            
        }
        public async Task<List<MachinePartAttempt>> GetMachinePartsAttempts()
        {
            return await  _repo.GetMachinePartsAttempts();    
        }  
    }
}
