using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DatingApp.Core.Models;
using DatingApp.Core.Repositories;
using Microsoft.Data.SqlClient;
using System;
using Microsoft.AspNetCore.Identity;
using System.Transactions;

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
            var userFrom = await _context.Users.AsNoTracking()
            .Select(user => new User
            {
                Id = user.Id,
                UserName = user.UserName,
                Name = user.Name,
                LastName = user.LastName
                ,SecurityStamp = user.SecurityStamp
                ,PasswordHash = user.PasswordHash
                ,Sexo = user.Sexo
                ,Status = user.Status
                ,Email = user.Email
                ,Roles = (from userRole in user.UserRoles
                         join role in _context.Roles
                         on userRole.RoleId
                         equals role.Id
                         where role.Status == 1
                         select new Role 
                                    { 
                                     Id = role.Id
                                     , Name = role.Name
                                     , Menus = (from roleMenu in role.RoleMenus
                                                join menu in _context.Menu
                                                on roleMenu.MenuId
                                                equals menu.Id
                                                where menu.Status == 1
                                                select new Menu
                                                {
                                                    Id = menu.Id
                                                    ,Path = menu.Path
                                                    ,Title = menu.Title
                                                    ,Icon = menu.Icon
                                                    ,ParentId = menu.ParentId
                                                    ,Status = menu.Status
                                                }).ToList()
                         }).ToList()
                ,UnAssignedRoles = (from roles in _context.Roles
                                    where !user.UserRoles.Any(ur => ur.RoleId == roles.Id)
                                    && roles.Status == 1
                                    select new Role
                                    {
                                        Id = roles.Id
                                        ,Name = roles.Name
                                    }).ToList()
            })
            .FirstOrDefaultAsync(user => user.Id == id); 
            
            return userFrom;
            
        } 
        public async Task<IEnumerable<User>> GetUsers()
        {
            //var roles = _userManager.GetRolesAsync()
            var users = await _userManager.Users.AsNoTracking()
            .Select(user => new User
            {
                Id = user.Id,
                UserName = user.UserName,
                Name = user.Name,
                LastName = user.LastName
                ,SecurityStamp = user.SecurityStamp
                ,Sexo = user.Sexo
                ,Status = user.Status
                ,Email = user.Email
            }).ToListAsync();

            return users; 
        }         
        public async Task<User> CreateUser(User User, String Password)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var result = await _userManager.CreateAsync(User, Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRolesAsync(User, User.RoleNames);
                }
                scope.Complete();
            }
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
            var userFrom = await _context.Users.AsNoTracking()
            .Select(user => new User
            {
                Id = user.Id
                ,UserName = user.UserName
                ,Name = user.Name
                ,LastName = user.LastName
                ,SecurityStamp = user.SecurityStamp
                ,PasswordHash = user.PasswordHash
                ,Sexo = user.Sexo
                ,Status = user.Status
                ,Email = user.Email
                ,Roles = (from userRole in user.UserRoles
                         join role in _context.Roles
                         on userRole.RoleId
                         equals role.Id
                         where role.Status == 1
                         select new Role
                         {
                             Id = role.Id
                                     ,
                             Name = role.Name
                                     ,
                             Menus = (from roleMenu in role.RoleMenus
                                      join menu in _context.Menu
                                      on roleMenu.MenuId
                                      equals menu.Id
                                      where menu.Status == 1
                                      select new Menu
                                      {
                                          Id = menu.Id
                                          ,
                                          Path = menu.Path
                                          ,
                                          Title = menu.Title
                                          ,
                                          Icon = menu.Icon
                                          ,
                                          ParentId = menu.ParentId
                                          ,
                                          Status = menu.Status
                                      }).ToList()
                         }).ToList()
                ,
                UnAssignedRoles = (from roles in _context.Roles
                                   where !user.UserRoles.Any(ur => ur.RoleId == roles.Id)
                                   && roles.Status == 1
                                   select new Role
                                   {
                                       Id = roles.Id
                                       ,
                                       Name = roles.Name
                                   }).ToList()
            })
            .FirstOrDefaultAsync(user => user.UserName == userName);

            return userFrom;

        }
        public async Task<User> ResetPassword(User user, string newPassword)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            return user;
        }
    }
}

