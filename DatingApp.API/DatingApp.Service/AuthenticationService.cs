﻿//using AutoMapper.Configuration;
using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Configuration.Binder;
using DatingApp.Core;
using DatingApp.Core.Models;
using DatingApp.Core.Services;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.Core.Repositories;
//using CloudinaryDotNet.Actions;
/*
The UserService class
Contains all Bussines Logic can be Injected many repositories that you considered necesary 
for make validations, rules, etc. Can declare specifies methods and functions 
declaring names and parameters that can be same of the IUserServiceRepository declarations
*/
/// <summary>
/// The UserService class
/// Contains all Bussines Logic can be Injected many repositories that you considered necesary 
/// for make validations, rules, etc. Can declare specifies methods and functions 
/// declaring names and parameters that can be same of the IUserServiceRepository declarations
/// </summary> 
/// 


namespace DatingApp.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;

        /// <summary>
        /// UserService receive a IUnitOfWork Interface that encapsulates all Names of the All repositories
        /// that the Data Layer has for Data Base Operations. 
        /// </summary>
        /// <param name="unitOfWork">The Interface IUnitOfWork </param>   
        public AuthenticationService(IUnitOfWork unitOfWork, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _config = config;
        }
        /// <summary>
        /// Get the first or Default User Register filter by Id 
        /// </summary>
        /// <param name="id">Id of User for the search</param>
        /// <returns>BaseResponse<User> with the result of the search By Id</returns>
        public async Task<BaseResponse<User>> Login(String userName, String password)
        {
            //Declare variables for result and errors for be filled with the response of _unitOfWork
            BaseResponse<User> result = new BaseResponse<User>();
            List<string> err = new List<string>();

            try
            {
                //Search for user filtering by userName
                var user = await _unitOfWork.UserRepository.GetUserByUserName(userName);

                if (user == null)
                {
                    //If doesn't exist userName
                    err.Add("El Usuario ingresado no existe ");
                    result.errors = err;
                    return result;
                    
                }

                var res = await _unitOfWork.AuthenticationRepository.CheckPasswordSignIn(user, password);

                if (!res.Succeeded)
                {
                    err.Add("El Password no coincide con el usuario ");
                    result.errors = err;
                    return result;
                }

                var token = (await GenerateJwtToken(user));

                //Using _unitOfWork call to a UserRepository and through of the 
                //GetByIdAsync GENERIC method Search the first or Default
                result.DataResponse = user;

                if (result.DataResponse != null)
                {
                    //result.DataResponse.RoleNames = result.DataResponse.UserRoles
                    //                            .Select(r => r.Role.Name)
                    //                            .ToList();
                    result.DataResponse.token = token;

                }

                //If the Query was Successful then in the result this flat in true
                result.Successful = true;
            }
            catch (System.Exception ex)
            {
                //If exist a Exception it's catched and Set in err List the Message 
                err.Add("Error en AuthenticationService -> Login " + ex.Message);
                //Set in the result errors object the exception message
                result.errors = err;
            }

            return result;
        }
        private async Task<string> GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}