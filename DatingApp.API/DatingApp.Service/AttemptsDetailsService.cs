
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
//using DatingApp.Api.Data;
using DatingApp.Core;
using DatingApp.Core.Models;
using DatingApp.Core.Services;


namespace DatingApp.Services
{
    public class AttemptsDetailsService : IAttemptsDetailsService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AttemptsDetailsService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<AttemptDetail> GetAttemptsDetailById(int id)
        {
            return await _unitOfWork.AttemptsDetailsRepository.GetByIdAsync(id);            
        }
        public async Task<IEnumerable<AttemptDetail>> GetAttemptsDetails()
        {
            return await  _unitOfWork.AttemptsDetailsRepository.GetAllAsync();    
        }        
        public async Task<AttemptDetail> CreateAttemptDetail(AttemptDetail AttemptDetailToCreate)
        {
            await  _unitOfWork.AttemptsDetailsRepository.AddAsync(AttemptDetailToCreate);    
            await  _unitOfWork.CommitAsync();
            return AttemptDetailToCreate;
        }
    }
}
