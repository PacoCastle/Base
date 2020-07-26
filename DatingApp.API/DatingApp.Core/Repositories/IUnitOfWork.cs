using System;
using System.Threading.Tasks;
using DatingApp.Core.Repositories;

namespace DatingApp.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IMachinePartsAttemptsRepository MachinePartsAttemptsRepository { get; }
        IAttemptsDetailsRepository AttemptsDetailsRepository { get; }
        IMachineRepository MachineRepository { get; }
        IPartRepository PartRepository { get; }
        Task<int> CommitAsync();
        
    }
}