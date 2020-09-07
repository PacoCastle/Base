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
/*
The PartModelsController class
Contains all EndPoints for Get, Post and Put PartModels Entity
*/
/// <summary>
/// The PartModelsRepository class
/// Contains all EndPoints for Get, Post and Put PartModels Entity
/// </summary>    
/// 
namespace DatingApp.API.Controllers
 {
    [AllowAnonymous]
     [Route("api/[controller]")]
     [ApiController]
     public class PartsController : ControllerBase
     {
         private readonly IMapper _mapper;

         private readonly IPartService _service;
         /// <summary>
         /// PartModelsController Constructor Initialize the Injected Interfaces for use it.
         /// </summary>
         /// <param name="mapper">Interface that contain mappings between DTO's and Models</param>
         /// <param name="service">Interface that contain acces to Service funtions and methods of PartService</param>
         public PartsController(IPartService service, IMapper mapper)
         {
             _mapper = mapper;
             _service = service;         
         }
        /// <summary>
        /// Search in PartModel filter By Id
        /// </summary>
        /// <param name="id">Id for Search Register in Data Base for </param>
        /// <returns>Object wit Status of execution and Data Searched and a Status of request</returns>
         [HttpGet("{id}", Name = "GetPart")]
         public async Task<IActionResult> GetPart(int id)
         {
              //Get ther response from call GetPartById from Service that retorn object with data for be validate
              var serviceResult  = await _service.GetPartById(id);

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
        /// Search All PartModel 
        /// </summary>
        /// <returns>Object wit Status of execution and Data Searched and a Status of request</returns>
         [HttpGet(Name = "GetParts")]
         public async Task<IActionResult> GetParts()
         {
              //Get ther response from call GetPartModels from Service that retorn object with data for be validate
              var serviceResult = await _service.GetParts();

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
        /// Create PartModel register
        /// </summary>
        /// <param name="PartForCreationDto">DTO That contains the properties for Insert in Data Base</param>
        /// <returns>Object wit Status of execution and Data Created and a Status of request</returns>
          [HttpPost]
         public async Task<IActionResult> CreatePart(PartForRegisterDto PartForCreationDto)
         {
            // Map to a PartModel for Send to Service
            var PartForCreate = _mapper.Map<PartModel>(PartForCreationDto); 

             //Get ther response from call GetPartModels from Service that retorn object with data for be validate
             var serviceResult = await _service.CreatePart(PartForCreate);

            //If the Service response Successful the Query was executed  and return the register Created
             if (serviceResult.Successful)
             {
                 return Ok(serviceResult);                 
             }
            //If the Service response isn't Successful then ocurred some wrong and return (400)
             return BadRequest(serviceResult);   
         }
         /// <summary>
        /// Update PartModel register
        /// </summary>
        /// <param name="id">Id of Register to be Updated</param>
        /// <param name="PartForUpdateDto">DTO that contains properties to be Updated </param>
        /// <returns>Object wit Status of execution and Data Updated and a Status of request</returns>
      [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePart(int id, PartForUpdateDto PartForUpdateDto)
        {
            //Search if de Id to be Updated get Data for Update
            var PartFromRepo = await _service.GetPartById(id);

            //Set in a var the Data that was Obtained in the last Step 
            var PartToBeUpdated = PartFromRepo.DataResponse;

            //If not exist Data for the id parameter return 404 and Empty DataResponse object 
            if (PartToBeUpdated == null)
                return NotFound();

            //If exist Data it's Mapped to a PartModel for Send to Service
            var PartForUpdate = _mapper.Map<PartModel>(PartForUpdateDto); 

            //Get Response from Service and UpdatePartModel sendig Object to be Updated and Data for make the Update 
            var serviceResult = await _service.UpdatePart(PartToBeUpdated, PartForUpdate);

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