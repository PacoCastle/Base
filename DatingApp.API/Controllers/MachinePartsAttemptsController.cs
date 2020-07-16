using System;
 using System.Collections.Generic;
 using System.Security.Claims;
 using System.Threading.Tasks;
 using AutoMapper;
 using DatingApp.API.Data;
 using DatingApp.API.Dtos;
 using DatingApp.API.Helpers;
 using DatingApp.API.Models;
using DatingApp.API.Services;
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
         public MachinePartsAttemptsController(IDatingRepository repo, IMapper mapper, IMachinePartsAttemptsService service)
         {
             _mapper = mapper;
             _repo = repo;
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
         public async Task<IActionResult> CreateMachinePartAttempt(MachPartAttemRegisterDto MachinePartsAttemptsForCreationDto)
         {
             //var MachinePartsAttempts = _mapper.Map<MachinePartAttempt>(MachinePartsAttemptsForCreationDto);

              var MachinePartsAttempts = await _repo.RegisterMachinePartAttempt(MachinePartsAttemptsForCreationDto);

              if (MachinePartsAttempts.Id > 0)
             {
                 var MachinePartsAttemptsForReturnDto = _mapper.Map<MachPartAttemReturnDto>(MachinePartsAttempts);

                 return Ok(MachinePartsAttemptsForReturnDto);
                 //return CreatedAtRoute("GetMachinePartsAttemptss",MachinePartsAttemptsForReturnDto);
             }

              throw new Exception("Creating the MachinePartsAttempts failed on save");
         } 

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMachinePartAttempt(int id, MachPartAttemUpdateDto MachPartAttemUpdateDto)
        {
            var MachPartAttemFromRepo = await _repo.GetMachinePartAttempt(id);

            _mapper.Map(MachPartAttemUpdateDto, MachPartAttemFromRepo);

            if (await _repo.SaveAll())
             {
                 var MachPartAttemReturnDto = _mapper.Map<MachPartAttemReturnDto>(MachPartAttemFromRepo);

                 return Ok(MachPartAttemReturnDto);
                 //return CreatedAtRoute("GetParts",PartForReturnDto);
             }

            throw new Exception($"Updating UpdateMachinePartAttempt {id} failed on save");
        }              
     }
 } 