using System;
 using System.Collections.Generic;
 using System.Security.Claims;
 using System.Threading.Tasks;
 using AutoMapper;
 using DatingApp.API.Data;
 using DatingApp.API.Dtos;
 using DatingApp.API.Helpers;
 using DatingApp.API.Models;
 using Microsoft.AspNetCore.Authorization;
 using Microsoft.AspNetCore.Mvc;
 
namespace DatingApp.API.Controllers
 {
    [AllowAnonymous]
     [Route("api/devices")]
     [ApiController]
     public class DeviceController : ControllerBase
     {
         private readonly IDatingRepository _repo;
         private readonly IMapper _mapper;
         public DeviceController(IDatingRepository repo, IMapper mapper)
         {
             _mapper = mapper;
             _repo = repo;             
         }

         [HttpGet]
         public async Task<IActionResult> GetDevices()
         {
              var devicesFromRepo = await _repo.GetDevices();

              //var devices = _mapper.Map<IEnumerable<DeviceConfigurationForReturnDto>>(devicesFromRepo);             
              
              return Ok(devicesFromRepo);
         }
         
         [HttpPost]
         public async Task<IActionResult> CreateDevice(List<ConfigurationForRegisterDto> configurationForRegisterDto)
         {             
            
            int _resultData = await _repo.AddByStored(configurationForRegisterDto);

            if (_resultData > 1)
            return Ok(configurationForRegisterDto);             

              throw new Exception("Creating the Configuration failed on save");
         }
         [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDevice(int id,  DeviceConfigurationForUpdateDto DeviceConfigurationForUpdateDto)
        {
            var DeviceFromRepo = await _repo.GetDevice(id);

            _mapper.Map(DeviceConfigurationForUpdateDto, DeviceFromRepo);

            if (await _repo.SaveAll())
             {
                 var PartForReturnDto = _mapper.Map<PartForReturnDto>(DeviceFromRepo);

                 return Ok(PartForReturnDto);
                 //return CreatedAtRoute("GetParts",PartForReturnDto);
             }

            throw new Exception($"Updating Part {id} failed on save");
        }              
     }
 } 