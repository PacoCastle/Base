using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DatingApp.Core.Models;
using DatingApp.Core.Repositories;
using Microsoft.Data.SqlClient;
using System;
using Microsoft.AspNetCore.Identity;

namespace DatingApp.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository 
    {
        private readonly UserManager<User> _userManager;

        private readonly DataContext _context;
        public UserRepository( UserManager<User> userManager, DataContext context)
        : base(context)
        { 
            _userManager = userManager;
            _context = context;
        }

        public async Task<User> GetUserById(int id)
        {
            User user = new User();
            //var roles = _userManager.GetRolesAsync()
            /* var userFrom = await _context.Users.Where(u =>
                 u.Id == id )
            .Select(u => new User()
            {
                Id = u.Id,
                UserName = u.UserName,
                Name = u.Name,
                LastName = u.LastName
                ,SecurityStamp = u.SecurityStamp
            })
            .FirstOrDefaultAsync().ConfigureAwait(false); */
            
              var menu = await _context.Menu
                .OrderBy(x => x.Id)
                .Select(menu => new
                {
                    Id = menu.Id,
                    UserName = menu.Title
                }).FirstOrDefaultAsync();  
            
            return user;
            
        } 
        public async Task<IEnumerable<User>> GetUsers()
        {
            //var roles = _userManager.GetRolesAsync()
            var users = await _userManager.Users
            .Select(u => new User()
            {
                Id = u.Id,
                //UserName = u.UserName,
                Name = u.Name,
                LastName = u.LastName
                ,SecurityStamp = u.SecurityStamp
            })
            .ToListAsync();

            //return await _userManager.FindByIdAsync(id.ToString());
            
            return users; 
        }         
        public async Task<User> CreateUser(User User, String Password)
        {
            var result = _userManager.CreateAsync(User, Password).Result;
            
            var userCreated = _userManager.FindByNameAsync(User.UserName).Result;

            _userManager.AddToRolesAsync(userCreated,User.RoleNames).Wait();
            
            return User; 
        }
        public async Task<User> GetUserRoles(User user)
        {
            user.RoleNames = await _userManager.GetRolesAsync(user);
            return user; 
        }

        public async Task<User> AddUserRoles(User user, IEnumerable<string> rolesForAdd, IEnumerable<string> rolesForExclude)
        {
            await _userManager.AddToRolesAsync(user, rolesForAdd.Except(rolesForExclude));
            return user; 
        }
        public async Task<User> RemoveUserRoles(User user, IEnumerable<string> rolesForRemove, IEnumerable<string> rolesForExclude)
        {
            await _userManager.RemoveFromRolesAsync(user, rolesForRemove.Except(rolesForExclude));
            return user; 
        }
        public async Task<User> UpdateUser(User userForBeUpdated)
        {
            IdentityResult result =  await _userManager.UpdateAsync(userForBeUpdated);

            //var userFromRepo = await GetUserById(userForBeUpdated.Id);
            
            return userForBeUpdated; 
        }
        public async Task<User> GetUserByUserName(string userName)
        {
            var userFromRepo = await _userManager.FindByNameAsync(userName);

            return userFromRepo;

        }
    }
}

