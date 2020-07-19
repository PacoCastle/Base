using System;
 using System.Collections.Generic;
 using System.Security.Claims;
 using System.Threading.Tasks;
 using AutoMapper;
 using DatingApp.API.Data;
 using DatingApp.API.Dtos;
 using DatingApp.API.Helpers;
 using DatingApp.Core.Models;
using DatingApp.Core.Services;
using Microsoft.AspNetCore.Authorization;
 using Microsoft.AspNetCore.Mvc;
 
namespace DatingApp.API.Controllers
 {
    [AllowAnonymous]
     [Route("api/[controller]")]
     [ApiController]
     public class MachinePartsAttemptsController : ControllerBase
     {
         private readonly IDatingRepository _repo;
         private readonly IMapper _mapper;

         private readonly IMachinePartsAttemptsService _service;
         public MachinePartsAttemptsController(IMapper mapper, IMachinePartsAttemptsService service)
         {
             _mapper = mapper;
             _service = service;             
         }

        [HttpGet("{id}", Name = "GetMachinePartsAttempt")]
         public async Task<IActionResult> GetMachinePartsAttempt(int id)
         {
              var MachinePartsAttemptFromRepo = await _service.GetMachinePartAttempt(id);

              if (MachinePartsAttemptFromRepo == null)
                 return NotFound();

              return Ok(MachinePartsAttemptFromRepo);
         }

         [HttpGet(Name = "GetMachinePartsAttempts")]
         public async Task<IActionResult> GetMachinePartsAttempts()
         {
              var MachinePartsAttemptsFromRepo = await _service.GetMachinePartsAttempts();

              var MachinePartsAttempts = _mapper.Map<IEnumerable<MachPartAttemReturnDto>>(MachinePartsAttemptsFromRepo);               
              
              return Ok(MachinePartsAttempts);
         }

        [HttpPost]
         public async Task<IActionResult> CreateMachinePartAttempt(MachPartAttemRegisterDto mpaCreateDto)
         {
             var MachinePartsAttempts = await _service.AddByStored(mpaCreateDto.MachineModel, mpaCreateDto.PartModel, mpaCreateDto.InternalSequence);

              if (MachinePartsAttempts > 0)
             {
                 //var MachinePartsAttemptsForReturnDto = _mapper.Map<MachPartAttemReturnDto>(mpaCreateDto);
                 //MachinePartsAttemptsForReturnDto.Id = MachinePartsAttempts;

                 return Ok(MachinePartsAttempts);
                 //return CreatedAtRoute("GetMachinePartsAttemptss",MachinePartsAttemptsForReturnDto);
             }

              throw new Exception("Creating the MachinePartsAttempts failed on save");
         } 

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMachinePartAttempt(int id, MachPartAttemUpdateDto MachPartAttemUpdateDto)
        {
            var MachPartAttemToBeUpdated = await _service.GetMachinePartAttempt(id);

            if (MachPartAttemToBeUpdated == null)
                return NotFound();

            var MachinePartAttempt = _mapper.Map<MachinePartAttempt>(MachPartAttemUpdateDto); 

            await _service.UpdateMachinePartAttempt(MachPartAttemToBeUpdated, MachinePartAttempt);

            var updatedMachinePartAttempt = await _service.GetMachinePartAttempt(id);
            var MachPartAttemReturn = _mapper.Map<MachPartAttemReturnDto>(updatedMachinePartAttempt);

            return Ok(MachPartAttemReturn);

            throw new Exception($"Updating UpdateMachinePartAttempt {id} failed on save");
        }              
     }
 } 