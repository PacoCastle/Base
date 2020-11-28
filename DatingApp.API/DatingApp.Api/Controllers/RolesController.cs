using System;
 using System.Collections.Generic;
 using System.Security.Claims;
 using System.Threading.Tasks;
 using AutoMapper;
 using DatingApp.Api.Data;
 using DatingApp.Api.Dtos;
 using DatingApp.Api.Helpers;
 using Microsoft.AspNetCore.Authorization;
 using Microsoft.AspNetCore.Mvc;
 using DatingApp.Core.Models;
using DatingApp.Core.Services;
using System.Net;
using DatingApp.Core;
/*
The RolesController class
Contains all EndPoints for Get, Post and Put Roles Entity
*/
/// <summary>
/// The RolesRepository class
/// Contains all EndPoints for Get, Post and Put Roles Entity
/// </summary>    
/// 
namespace DatingApp.Api.Controllers
 {
    [AllowAnonymous]
     [Route("api/[controller]")]
     [ApiController]
     public class RolesController : ControllerBase
     {
         private readonly IMapper _mapper;

         private readonly IRoleService _service;

         private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// RolesController Constructor Initialize the Injected Interfaces for use it.
        /// </summary>
        /// <param name="mapper">Interface that contain mappings between DTO's and Models</param>
        /// <param name="service">Interface that contain acces to Service funtions and methods of RoleService</param>
        public RolesController(IRoleService service, IMapper mapper, IUnitOfWork unitOfWork)
         {
             _mapper = mapper;
             _service = service;
            _unitOfWork = unitOfWork;
         }
        /// <summary>
        /// Search in Role filter By Id
        /// </summary>
        /// <param name="id">Id for Search Register in Data Base for </param>
        /// <returns>Object wit Status of execution and Data Searched and a Status of request</returns>
         [HttpGet("{id}", Name = "GetRole")]
         public async Task<IActionResult> GetRole(int id)
         {
              //Get ther response from call GetRoleById from Service that retorn object with data for be validate
              var serviceResult  = await _service.GetRoleById(id);

              //If the Service response Successful the Query was executed
              if (serviceResult.Successful)
              {
                  //If the search doesn't has Data filtering by Id then return NotFound (404)
                  if (serviceResult.DataResponse == null)
                    return NotFound(serviceResult);

                  //If the search has Data filtering by Id then return Ok (200)  and return the register Serached
                  return Ok(serviceResult);

              }
              //If the Service response isn't Successful then ocurred some wrong and return (400)
              return BadRequest(serviceResult); 
         }
        /// <summary>
        /// Search All Role 
        /// </summary>
        /// <returns>Object wit Status of execution and Data Searched and a Status of request</returns>
         [HttpGet(Name = "GetRoles")]
         public async Task<IActionResult> GetRoles()
         {
              //Get ther response from call GetRoles from Service that retorn object with data for be validate
              var serviceResult = await _service.GetRoles();

              //If the Service response Successful the Query was executed  
              if (serviceResult.Successful)
              {
                  //If the search doesn't has Data filtering by Id then return NotFound (404)
                  if (serviceResult.DataResponse == null)
                    return NotFound(serviceResult);
                
                    //If the search has Data filtering by Id then return Ok (200) and return the register Serached
                  return Ok(serviceResult);
              }
              //If the Service response isn't Successful then ocurred some wrong and return (400)
              return BadRequest(serviceResult);     
         }
        /// <summary>
        /// Create Role register
        /// </summary>
        /// <param name="RoleForCreateDto">DTO That contains the properties for Insert in Data Base</param>
        /// <returns>Object wit Status of execution and Data Created and a Status of request</returns>
          [HttpPost]
         public async Task<IActionResult> CreateRole(RoleForCreateDto RoleForCreateDto)
         {
            try
            {
                List<string> err = new List<string>();

                //Search if de Id to be Updated get Data for Update
                var RoleFromRepo = await _service.GetRoleByName(RoleForCreateDto.Name);

                //If not exist Data for the id parameter return 404 and Empty DataResponse object 
                if (RoleFromRepo.DataResponse != null)
                {
                    err.Add("El rol " + RoleForCreateDto.Name + " ya existe");
                    RoleFromRepo.errors = err;
                    return Conflict(RoleFromRepo);
                }

                // Map to a Role for Send to Service
                var RoleForCreate = _mapper.Map<Role>(RoleForCreateDto);

                //Get ther response from call GetRoles from Service that retorn object with data for be validate
                var serviceResult = await _service.CreateRole(RoleForCreate);

                //If the Service response Successful the Query was executed  and return the register Created
                if (serviceResult.Successful)
                {
                    return Ok(serviceResult);
                }
                //If the Service response isn't Successful then ocurred some wrong and return (400)
                return BadRequest(serviceResult);
            }
            catch (Exception ex )
            {

                throw;
            }
            
         }
        /// <summary>
        /// Update Role register
        /// </summary>
        /// <param name="id">Id of Register to be Updated</param>
        /// <param name="RoleForUpdateDto">DTO that contains properties to be Updated </param>
        /// <returns>Object wit Status of execution and Data Updated and a Status of request</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id , RoleForUpdateDto RoleForUpdateDto)
        {
            //Search if de Id to be Updated get Data for Update
            //var RoleFromRepo = await _service.GetRoleById(id);
            try
            {

                var RoleFromRepo = await _unitOfWork.RoleRepository.GetRoleById(id);
                var RoleToBeUpdated = await _unitOfWork.RoleRepository.GetByIdAsync(RoleFromRepo.Id);

                //If not exist Data for the id parameter return 404 and Empty DataResponse object 
                if (RoleToBeUpdated == null)
                    return NotFound();

                //If exist Data it's Mapped to a User for Send to Service
                var RoleForUpdate = _mapper.Map<Role>(RoleForUpdateDto);

                //Get Response from Service and UpdateRole sendig Object to be Updated and Data for make the Update 
                var serviceResult = await _service.UpdateRole(RoleToBeUpdated, RoleForUpdate);

                if (serviceResult.Successful)
                {
                    return Ok(serviceResult);
                }

                //if the Update wasn't Successful then return 400 and an Object with Error information
                return BadRequest(serviceResult);
            }
            catch (Exception ex)
            {

                throw;
            } 
        }
    } 
 } 