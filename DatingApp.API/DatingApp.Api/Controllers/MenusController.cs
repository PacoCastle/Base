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
/*
The MenusController class
Contains all EndPoints for Get, Post and Put Menus Entity
*/
/// <summary>
/// The MenusRepository class
/// Contains all EndPoints for Get, Post and Put Menus Entity
/// </summary>    
/// 
namespace DatingApp.Api.Controllers
 {
    [AllowAnonymous]
     [Route("api/[controller]")]
     [ApiController]
     public class MenusController : ControllerBase
     {
         private readonly IMapper _mapper;

         private readonly IMenuService _service;
         /// <summary>
         /// MenusController Constructor Initialize the Injected Interfaces for use it.
         /// </summary>
         /// <param name="mapper">Interface that contain mappings between DTO's and Models</param>
         /// <param name="service">Interface that contain acces to Service funtions and methods of MenuService</param>
         public MenusController(IMenuService service, IMapper mapper)
         {
             _mapper = mapper;
             _service = service;         
         }
        /// <summary>
        /// Search in Menu filter By Id
        /// </summary>
        /// <param name="id">Id for Search Register in Data Base for </param>
        /// <returns>Object wit Status of execution and Data Searched and a Status of request</returns>
         [HttpGet("{id}", Name = "GetMenu")]
         public async Task<IActionResult> GetMenu(int id)
         {
              //Get ther response from call GetMenuById from Service that retorn object with data for be validate
              var serviceResult  = await _service.GetMenuById(id);

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
        /// Search All Menu 
        /// </summary>
        /// <returns>Object wit Status of execution and Data Searched and a Status of request</returns>
         [HttpGet(Name = "GetMenus")]
         public async Task<IActionResult> GetMenus()
         {
             BaseResponse<IEnumerable<MenuReturnDto>> resultMapped = new BaseResponse<IEnumerable<MenuReturnDto>>();
              //Get ther response from call GetMenus from Service that retorn object with data for be validate
              var serviceResult = await _service.GetMenus();

              //If the Service response Successful the Query was executed  
              if (serviceResult.Successful)
              {
                  //If the search doesn't has Data filtering by Id then return NotFound (404)
                  if (serviceResult.DataResponse == null)
                    return NotFound(serviceResult);
                    
                  var dataMapped = _mapper.Map<IEnumerable<MenuReturnDto>>(serviceResult.DataResponse);
                  
                  resultMapped.DataResponse = dataMapped;
                    //If the search has Data filtering by Id then return Ok (200) and return the register Serached
                  return Ok(resultMapped);
              }
              //If the Service response isn't Successful then ocurred some wrong and return (400)
              return BadRequest(serviceResult);     
         }
        /// <summary>
        /// Create Menu register
        /// </summary>
        /// <param name="MenuForCreationDto">DTO That contains the properties for Insert in Data Base</param>
        /// <returns>Object wit Status of execution and Data Created and a Status of request</returns>
          [HttpPost]
         public async Task<IActionResult> CreateMenu(MenuCreateDto MenuForCreationDto)
         {
            // Map to a Menu for Send to Service
            var MenuForCreate = _mapper.Map<Menu>(MenuForCreationDto); 

             //Get ther response from call GetMenus from Service that retorn object with data for be validate
             var serviceResult = await _service.CreateMenu(MenuForCreate);

            //If the Service response Successful the Query was executed  and return the register Created
             if (serviceResult.Successful)
             {
                 return Ok(serviceResult);                 
             }
            //If the Service response isn't Successful then ocurred some wrong and return (400)
             return BadRequest(serviceResult);   
         }
         /// <summary>
        /// Update Menu register
        /// </summary>
        /// <param name="id">Id of Register to be Updated</param>
        /// <param name="MenuForUpdateDto">DTO that contains properties to be Updated </param>
        /// <returns>Object wit Status of execution and Data Updated and a Status of request</returns>
      [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMenu(int id, MenuUpdateDto MenuForUpdateDto)
        {
            //Search if de Id to be Updated get Data for Update
            var MenuFromRepo = await _service.GetMenuById(id);

            //Set in a var the Data that was Obtained in the last Step 
            var MenuToBeUpdated = MenuFromRepo.DataResponse;

            //If not exist Data for the id parameter return 404 and Empty DataResponse object 
            if (MenuToBeUpdated == null)
                return NotFound();

            //If exist Data it's Mapped to a Menu for Send to Service
            var MenuForUpdate = _mapper.Map<Menu>(MenuForUpdateDto); 

            //Get Response from Service and UpdateMenu sendig Object to be Updated and Data for make the Update 
            var serviceResult = await _service.UpdateMenu(MenuToBeUpdated, MenuForUpdate);

            //if the Update was Successful then return 200 and an Object with DataResponse Updated
            if(serviceResult.Successful)
            {
                return Ok(serviceResult);
            }        

            //if the Update wasn't Successful then return 400 and an Object with Error information
            return BadRequest(serviceResult);   
        }         
     }
 } 