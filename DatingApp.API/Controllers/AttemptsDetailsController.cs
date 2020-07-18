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
 using DatingApp.Core;
 
namespace DatingApp.API.Controllers
 {
    [AllowAnonymous]
     [Route("api/[controller]")]
     [ApiController]
     public class AttemptsDetailsController : ControllerBase
     {
         private readonly IDatingRepository _repo;
         private readonly IMapper _mapper;
         public AttemptsDetailsController(IDatingRepository repo, IMapper mapper)
         {
             _mapper = mapper;
             _repo = repo;             
         }

         [HttpGet("{id}", Name = "GetAttemptsDetail")]
         public async Task<IActionResult> GetAttemptsDetail(int id)
         {
              var AttemptDetailFromRepo = await _repo.GetAttemptDetail(id);

              if (AttemptDetailFromRepo == null)
                 return NotFound();

              return Ok(AttemptDetailFromRepo);
         }

         [HttpGet(Name = "GetAttemptsDetails")]
         public async Task<IActionResult> GetAttemptsDetails()
         {
              var AttemptDetailsFromRepo = await _repo.GetAttemptDetails();

              var AttemptDetails = _mapper.Map<IEnumerable<AttemptDetailReturnDto>>(AttemptDetailsFromRepo);               
              
              return Ok(AttemptDetails);
         }

          [HttpPost]
         public async Task<IActionResult> CreateAttemptDetail(AttemptDetailRegisterDto AttemptDetailRegisterDto)
         {
             var AttemptDetail = _mapper.Map<AttemptDetail>(AttemptDetailRegisterDto);

              _repo.Add(AttemptDetail);

              if (await _repo.SaveAll())
             {
                 var AttemptDetailReturnDto = _mapper.Map<AttemptDetailReturnDto>(AttemptDetail);

                 return Ok(AttemptDetailReturnDto);
                 //return CreatedAtRoute("GetParts",PartForReturnDto);
             }

              throw new Exception("Creating the CreateAttemptDetail failed on save");
         }               
     }
 } 