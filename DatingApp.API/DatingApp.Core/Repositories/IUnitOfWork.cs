using System;
using System.Threading.Tasks;
using DatingApp.Core.Repositories;

namespace DatingApp.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IMachinePartsAttemptsRepository MachinePartsAttemptsRepository { get; }
        Task<int> CommitAsync();
        
    }
}