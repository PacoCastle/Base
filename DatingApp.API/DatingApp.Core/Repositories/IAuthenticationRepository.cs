using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace DatingApp.Core.Repositories
{
    public interface IAuthenticationRepository 
    {
        Task<SignInResult> CheckPasswordSignIn(User user, string password);
    }
}