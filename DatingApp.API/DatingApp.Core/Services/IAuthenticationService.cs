using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.Core.Models;

namespace DatingApp.Core.Services
{
    public interface IAuthenticationService
    {
        Task<BaseResponse<User>>  Login(String userName, String password);
    }
}
