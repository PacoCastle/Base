using System.Threading.Tasks;
using DatingApp.Core;
using DatingApp.Core.Repositories;
using DatingApp.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UDataContext _context;
        private MachinePartsAttemptsRepository _machinePartsAttemptsRepository;        

        public UnitOfWork(UDataContext context)
        {
            this._context = context;
        }

        public IMachinePartsAttemptsRepository MachinePartsAttemptsRepository => _machinePartsAttemptsRepository = _machinePartsAttemptsRepository ?? new MachinePartsAttemptsRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}