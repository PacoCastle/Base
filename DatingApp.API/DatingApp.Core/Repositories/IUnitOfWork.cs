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
        IMenuRepository MenuRepository { get; }
        IRoleRepository RoleRepository { get; }
        IUserRepository UserRepository { get; }
        Task<int> CommitAsync();
        
    }
}