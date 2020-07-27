using System.Threading.Tasks;
using DatingApp.Core;
using DatingApp.Core.Repositories;
using DatingApp.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private MachinePartsAttemptsRepository _machinePartsAttemptsRepository;        
        private AttemptsDetailsRepository _attemptsDetailsRepository;
        private MachineRepository _machineRepository;

        private PartRepository _partRepository;

        public UnitOfWork(DataContext context)
        {
            this._context = context;
        }

        public IMachinePartsAttemptsRepository MachinePartsAttemptsRepository => _machinePartsAttemptsRepository = _machinePartsAttemptsRepository ?? new MachinePartsAttemptsRepository(_context);
        public IAttemptsDetailsRepository AttemptsDetailsRepository => _attemptsDetailsRepository = _attemptsDetailsRepository ?? new AttemptsDetailsRepository(_context);
        public IMachineRepository MachineRepository => _machineRepository = _machineRepository ?? new MachineRepository(_context);
        public IPartRepository PartRepository => _partRepository = _partRepository ?? new PartRepository(_context);

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