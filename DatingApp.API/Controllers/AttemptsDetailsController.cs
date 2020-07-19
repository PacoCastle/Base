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

namespace DatingApp.API.Controllers
 {
    [AllowAnonymous]
     [Route("api/[controller]")]
     [ApiController]
     public class AttemptsDetailsController : ControllerBase
     {
         private readonly IAttemptsDetailsService _service;
         private readonly IMapper _mapper;
         public AttemptsDetailsController(IAttemptsDetailsService service , IMapper mapper)
         {
             _mapper = mapper;
             _service = service;             
         }

         [HttpGet("{id}", Name = "GetAttemptsDetail")]
         public async Task<IActionResult> GetAttemptsDetailById(int id)
         {
              var AttemptDetailFromRepo = await _service.GetAttemptsDetailById(id);

              if (AttemptDetailFromRepo == null)
                 return NotFound();

              return Ok(AttemptDetailFromRepo);
         }

         [HttpGet(Name = "GetAttemptsDetails")]
         public async Task<IActionResult> GetAttemptsDetails()
         {
              var AttemptDetailsFromRepo = await _service.GetAttemptsDetails();

              var AttemptDetails = _mapper.Map<IEnumerable<AttemptDetailReturnDto>>(AttemptDetailsFromRepo);               
              
              return Ok(AttemptDetails);
         }

          [HttpPost]
         public async Task<IActionResult> CreateAttemptDetail(AttemptDetailRegisterDto AttemptDetailRegisterDto)
         {
             var AttemptDetailToBeCreated = _mapper.Map<AttemptDetail>(AttemptDetailRegisterDto);

              var AttemptDetailCreated = await _service.CreateAttemptDetail(AttemptDetailToBeCreated);

              var AttemptDetailFromRepo = await _service.GetAttemptsDetailById(AttemptDetailCreated.Id); 

              var AttemptDetailReturn = _mapper.Map<AttemptDetailReturnDto>(AttemptDetailFromRepo);       

              return Ok(AttemptDetailReturn);

              throw new Exception("Creating the CreateAttemptDetail failed on save");
         }               
     }
 } 