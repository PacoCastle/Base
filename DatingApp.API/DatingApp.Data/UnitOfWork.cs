using System.Threading.Tasks;
using DatingApp.Core;
using DatingApp.Core.Models;
using DatingApp.Core.Repositories;
using DatingApp.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private MachinePartsAttemptsRepository _machinePartsAttemptsRepository;        
        private AttemptsDetailsRepository _attemptsDetailsRepository;
        private MachineRepository _machineRepository;

        private PartRepository _partRepository;
        private MenuRepository _menuRepository;
        private RoleRepository _roleRepository;
        private UserRepository _userRepository;
        private AuthenticationRepository _authenticationRepository;
        private RoleMenuRepository _roleMenuRepository;



        public UnitOfWork(DataContext context, RoleManager<Role> roleManager, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this._context = context;
            this._roleManager = roleManager;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        public IMachinePartsAttemptsRepository MachinePartsAttemptsRepository => _machinePartsAttemptsRepository = _machinePartsAttemptsRepository ?? new MachinePartsAttemptsRepository(_context);
        public IAttemptsDetailsRepository AttemptsDetailsRepository => _attemptsDetailsRepository = _attemptsDetailsRepository ?? new AttemptsDetailsRepository(_context);
        public IMachineRepository MachineRepository => _machineRepository = _machineRepository ?? new MachineRepository(_context);
        public IPartRepository PartRepository => _partRepository = _partRepository ?? new PartRepository(_context);   
        public IMenuRepository MenuRepository => _menuRepository = _menuRepository ?? new MenuRepository(_context);   
        public IRoleRepository RoleRepository => _roleRepository = _roleRepository ?? new RoleRepository(_roleManager,_context);    
        public IUserRepository UserRepository => _userRepository = _userRepository ?? new UserRepository(_userManager, _context);
        public IAuthenticationRepository AuthenticationRepository => _authenticationRepository = _authenticationRepository ?? new AuthenticationRepository(_signInManager, _context);

        public IRoleMenuRepository RoleMenuRepository => _roleMenuRepository = _roleMenuRepository ?? new RoleMenuRepository(_context);

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