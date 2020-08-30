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
    public class AuthenticationRepository : Repository<User>, IAuthenticationRepository
    {
        private readonly SignInManager<User> _signInManager;

        private readonly DataContext _context;
        public AuthenticationRepository(SignInManager<User> signInManager, DataContext context)
        : base(context)
        {
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<SignInResult> CheckPasswordSignIn(User user, string password)
        {

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);


            return result;

        }
    }
}


