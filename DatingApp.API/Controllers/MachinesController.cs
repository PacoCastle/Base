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
 
namespace DatingApp.API.Controllers
 {
    [AllowAnonymous]
     [Route("api/[controller]")]
     [ApiController]
     public class MachinesController : ControllerBase
     {
         private readonly IDatingRepository _repo;
         private readonly IMapper _mapper;
         public MachinesController(IDatingRepository repo, IMapper mapper)
         {
             _mapper = mapper;
             _repo = repo;             
         }

         [HttpGet("{id}", Name = "GetMachine")]
         public async Task<IActionResult> GetMachine(int id)
         {
              var MachineFromRepo = await _repo.GetMachine(id);

              if (MachineFromRepo == null)
                 return NotFound();

              return Ok(MachineFromRepo);
         }

         [HttpGet(Name = "GetMachines")]
         public async Task<IActionResult> GetMachines()
         {
              var MachinesFromRepo = await _repo.GetMachines();

              var Machines = _mapper.Map<IEnumerable<MachineReturnDto>>(MachinesFromRepo);               
              
              return Ok(Machines);
         }

          [HttpPost]
         public async Task<IActionResult> CreateMachine(MachineRegisterDto MachineForCreationDto)
         {
             var Machine = _mapper.Map<MachineModel>(MachineForCreationDto);

              _repo.Add(Machine);

              if (await _repo.SaveAll())
             {
                 var MachineForReturnDto = _mapper.Map<MachineReturnDto>(Machine);

                 return Ok(MachineForReturnDto);
                 //return CreatedAtRoute("GetMachines",MachineForReturnDto);
             }

              throw new Exception("Creating the Machine failed on save");
         }
      [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMachine(int id, MachineUpdateDto MachineForUpdateDto)
        {
            var MachineFromRepo = await _repo.GetMachine(id);

            _mapper.Map(MachineForUpdateDto, MachineFromRepo);

            if (await _repo.SaveAll())
             {
                 var MachineForReturnDto = _mapper.Map<MachineReturnDto>(MachineFromRepo);

                 return Ok(MachineForReturnDto);
                 //return CreatedAtRoute("GetMachines",MachineForReturnDto);
             }

            throw new Exception($"Updating Machine {id} failed on save");
        }         
     }
 } 