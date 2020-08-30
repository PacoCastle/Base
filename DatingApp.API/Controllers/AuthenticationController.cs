using System;
 using System.Collections.Generic;
 using System.Security.Claims;
 using System.Threading.Tasks;
 using AutoMapper;
 using DatingApp.API.Data;
 using DatingApp.API.Dtos;
 using DatingApp.API.Helpers;
 using Microsoft.AspNetCore.Authorization;
 using Microsoft.AspNetCore.Mvc;
 using DatingApp.Core.Models;
using DatingApp.Core.Services;
using System.Net;
/*
The RolesController class
Contains all EndPoints for Get, Post and Put Roles Entity
*/
/// <summary>
/// The RolesRepository class
/// Contains all EndPoints for Get, Post and Put Roles Entity
/// </summary>    
/// 
namespace DatingApp.API.Controllers
 {
    [AllowAnonymous]
     [Route("api/[controller]")]
     [ApiController]
     public class AuthenticationController : ControllerBase
     {
         private readonly IMapper _mapper;

         private readonly IAuthenticationService _service;
         /// <summary>
         /// RolesController Constructor Initialize the Injected Interfaces for use it.
         /// </summary>
         /// <param name="mapper">Interface that contain mappings between DTO's and Models</param>
         /// <param name="service">Interface that contain acces to Service funtions and methods of RoleService</param>
         public AuthenticationController(IAuthenticationService service, IMapper mapper)
         {
             _mapper = mapper;
             _service = service;         
         }
        /// <summary>
        /// Create Role register
        /// </summary>
        /// <param name="UserForLoginDto">DTO That contains the properties for Insert in Data Base</param>
        /// <returns>Object wit Status of execution and Data Created and a Status of request</returns>
        [HttpPost]
         public async Task<IActionResult> Login(UserForLoginDto userForLogin)
         {
             //Search if de Id to be Updated get Data for Update
            var serviceResult = await _service.Login(userForLogin.Username, userForLogin.Password);

            //If the Service response Successful the Query was executed  and return the register Created
             if (serviceResult.Successful)
             {
                 return Ok(serviceResult);                 
             }
            //If the Service response isn't Successful then ocurred some wrong and return (400)
             return BadRequest(serviceResult);   
         }
     } 
 } 