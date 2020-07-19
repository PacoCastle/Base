﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
//using DatingApp.API.Data;
using DatingApp.Core;
using DatingApp.Core.Models;
using DatingApp.Core.Services;


namespace DatingApp.Services
{
    public class MachinePartsAttemptsService : IMachinePartsAttemptsService
    {
        private readonly IUnitOfWork _unitOfWork;
        public MachinePartsAttemptsService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<MachinePartAttempt> GetMachinePartAttempt(int id)
        {
            return await _unitOfWork.MachinePartsAttempts.GetByIdAsync(id);
            
        }
        public async Task<IEnumerable<MachinePartAttempt>> GetMachinePartsAttempts()
        {
            return await  _unitOfWork.MachinePartsAttempts.GetAllAsync();    
        }  
        
        public async Task<int> AddByStored(string MachineModel, string PartModel, string InternalSequence)
        {
            return await  _unitOfWork.MachinePartsAttempts.AddByStored(MachineModel, PartModel, InternalSequence);    
        }  
    }
}