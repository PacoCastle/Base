using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Dtos;
using DatingApp.Core.Models;
using DatingApp.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
 /*
The MachinePartsAttemptsController class
Contains all EndPoints for Get, Post and Put MachinePartsAttempts Entity
*/
/// <summary>
/// The MachinePartsAttemptsRepository class
/// Contains all EndPoints for Get, Post and Put MachinePartsAttempts Entity
/// </summary>    
/// 
namespace DatingApp.API.Controllers
 {
    [AllowAnonymous]
     [Route("api/[controller]")]
     [ApiController]
     public class MachinePartsAttemptsController : ControllerBase
     {
         private readonly IMapper _mapper;

         private readonly IMachinePartsAttemptsService _service;
         /// <summary>
         /// MachinePartsAttemptsController Constructor Initialize the Injected Interfaces for use it.
         /// </summary>
         /// <param name="mapper">Interface that contain mappings between DTO's and Models</param>
         /// <param name="service">Interface that contain acces to Service funtions and methods of MachinePartsAttemptsService</param>
         public MachinePartsAttemptsController(IMapper mapper, IMachinePartsAttemptsService service)
         {
             _mapper = mapper;
             _service = service;             
         }
        /// <summary>
        /// Search in MachinePartsAttempt filter By Id
        /// </summary>
        /// <param name="id">Id for Search Register in Data Base for </param>
        /// <returns>Object wit Status of execution and Data Searched and a Status of request</returns>
        [HttpGet("{id}", Name = "GetMachinePartsAttempt")]
         public async Task<IActionResult> GetMachinePartsAttempt(int id)
         {
             //Get ther response from call GetMachinePartAttempt from Service that retorn object with data for be validate
              var serviceResult  = await _service.GetMachinePartAttempt(id);

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
        /// Search All MachinePartsAttempt 
        /// </summary>
        /// <returns>Object wit Status of execution and Data Searched and a Status of request</returns>
         [HttpGet(Name = "GetMachinePartsAttempts")]
         public async Task<IActionResult> GetMachinePartsAttempts()
         {
             //Get ther response from call GetMachinePartsAttempts from Service that retorn object with data for be validate
              var serviceResult = await _service.GetMachinePartsAttempts();

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
        /// Create MachinePartsAttempt register
        /// </summary>
        /// <param name="mpaCreateDto">DTO That contains the properties for Insert in Data Base</param>
        /// <returns>Object wit Status of execution and Data Created and a Status of request</returns>
        [HttpPost]
         public async Task<IActionResult> CreateMachinePartAttempt(MachPartAttemCreateDto mpaCreateDto)
         {
             //Get ther response from call GetMachinePartsAttempts from Service that retorn object with data for be validate
             var serviceResult = await _service.AddByStored(mpaCreateDto.MachineModel, mpaCreateDto.PartModel, mpaCreateDto.InternalSequence);
            //If the Service response Successful the Query was executed  and return the register Created
             if (serviceResult.Successful)
             {
                 return Ok(serviceResult);                 
             }
            //If the Service response isn't Successful then ocurred some wrong and return (400)
             return BadRequest(serviceResult);            
         } 
        /// <summary>
        /// Update MachinePartsAttempt register
        /// </summary>
        /// <param name="id">Id of Register to be Updated</param>
        /// <param name="MachPartAttemUpdateDto">DTO that contains properties to be Updated </param>
        /// <returns>Object wit Status of execution and Data Updated and a Status of request</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMachinePartAttempt(int id, MachPartAttemUpdateDto MachPartAttemUpdateDto)
        {
            //Search if de Id to be Updated get Data for Update
            var MachPartAttemFromRepo = await _service.GetMachinePartAttempt(id);

            //Set in a var the Data that was Obtained in the last Step 
            var MachPartAttemToBeUpdated = MachPartAttemFromRepo.DataResponse;

            //If not exist Data for the id parameter return 404 and Empty DataResponse object 
            if (MachPartAttemToBeUpdated == null)
                return NotFound();

            //If exist Data it's Mapped to a MachinePartAttempt for Send to Service
            var MachinePartAttemptForUpdate = _mapper.Map<MachinePartAttempt>(MachPartAttemUpdateDto); 

            //Get Response from Service and UpdateMachinePartAttempt sendig Object to be Updated and Data for make the Update 
            var serviceResult = await _service.UpdateMachinePartAttempt(MachPartAttemToBeUpdated, MachinePartAttemptForUpdate);

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