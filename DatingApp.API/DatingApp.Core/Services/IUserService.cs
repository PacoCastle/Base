﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.Core.Models;

namespace DatingApp.Core.Services
{
    public interface IUserService
    {
        Task<BaseResponse<IEnumerable<User>>> GetUsers();
        Task<BaseResponse<User>> GetUserById(int id);
        Task<BaseResponse<User>>  CreateUser(User user, String password);
        Task<BaseResponse<User>>  UpdateUser(User UserToBeUpdated , User UserForUpdate);
        Task<BaseResponse<User>> GetUserByUserName(string userName);

    }
}