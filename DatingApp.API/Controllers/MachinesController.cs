using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DatingApp.Core.Models;
using DatingApp.Core.Services;

namespace DatingApp.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class MachinesController : ControllerBase
    {
        private readonly IMapper _mapper;

        private readonly IMachineService _service;
        public MachinesController(IMapper mapper, IMachineService service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet("{id}", Name = "GetMachine")]
        public async Task<IActionResult> GetMachine(int id)
        {
            var MachineFromRepo = await _service.GetMachineById(id);

            if (MachineFromRepo == null)
                return NotFound();

            return Ok(MachineFromRepo);
        }

        [HttpGet(Name = "GetMachines")]
        public async Task<IActionResult> GetMachines()
        {
            var MachinesFromRepo = await _service.GetMachines();

            var Machines = _mapper.Map<IEnumerable<MachineReturnDto>>(MachinesFromRepo);

            return Ok(Machines);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMachine(MachineRegisterDto MachineForCreationDto)
        {
            var Machine = _mapper.Map<MachineModel>(MachineForCreationDto);
            var MachineCreate = await _service.CreateMachine(Machine);
            var MachineFromRepo = await _service.GetMachineById(MachineCreate.Id);
            var MachineReturn = _mapper.Map<MachineReturnDto>(Machine);

            return Ok(MachineReturn);
          
            throw new Exception("Creating the Machine failed on save");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMachine(int id, MachineUpdateDto MachineForUpdateDto)
        {
            var MachineToBeUpdate = await _service.GetMachineById(id);

            if (MachineToBeUpdate == null)
                return NotFound();


            var Machine = _mapper.Map<MachineModel>(MachineForUpdateDto);
            await _service.UpdateMachine(MachineToBeUpdate, Machine);

            var updateMachine = await _service.GetMachineById(id);
            var MachineReturn = _mapper.Map<MachineUpdateDto>(updateMachine);

            return Ok(MachineReturn);

            throw new Exception($"Updating Machine {id} failed on save");
        }
    }
}