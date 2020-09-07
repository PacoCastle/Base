using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.Core.Models;

namespace DatingApp.Core.Services
{
    public interface IAttemptsDetailsService
    {
        Task<AttemptDetail> GetAttemptsDetailById(int id);
        Task<IEnumerable<AttemptDetail>> GetAttemptsDetails();  
        Task<AttemptDetail> CreateAttemptDetail(AttemptDetail AttemptDetailToBeCreated);        
    }
}
