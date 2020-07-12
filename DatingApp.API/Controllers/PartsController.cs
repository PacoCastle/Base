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
     [Route("api/[controller]")]
     [ApiController]
     public class PartsController : ControllerBase
     {
         private readonly IDatingRepository _repo;
         private readonly IMapper _mapper;
         public PartsController(IDatingRepository repo, IMapper mapper)
         {
             _mapper = mapper;
             _repo = repo;             
         }

         [HttpGet("{id}", Name = "GetPart")]
         public async Task<IActionResult> GetPart(int id)
         {
              var PartFromRepo = await _repo.GetPart(id);

              if (PartFromRepo == null)
                 return NotFound();

              return Ok(PartFromRepo);
         }

         [HttpGet(Name = "GetParts")]
         public async Task<IActionResult> GetParts()
         {
              var PartsFromRepo = await _repo.GetParts();

              var Parts = _mapper.Map<IEnumerable<PartForReturnDto>>(PartsFromRepo);               
              
              return Ok(Parts);
         }

          [HttpPost]
         public async Task<IActionResult> CreatePart(PartForRegisterDto PartForCreationDto)
         {
             var Part = _mapper.Map<PartModel>(PartForCreationDto);

              _repo.Add(Part);

              if (await _repo.SaveAll())
             {
                 var PartForReturnDto = _mapper.Map<PartForReturnDto>(Part);

                 return Ok(PartForReturnDto);
                 //return CreatedAtRoute("GetParts",PartForReturnDto);
             }

              throw new Exception("Creating the Part failed on save");
         }
      [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePart(int id, PartForUpdateDto PartForUpdateDto)
        {
            var PartFromRepo = await _repo.GetPart(id);

            _mapper.Map(PartForUpdateDto, PartFromRepo);

            if (await _repo.SaveAll())
             {
                 var PartForReturnDto = _mapper.Map<PartForReturnDto>(PartFromRepo);

                 return Ok(PartForReturnDto);
                 //return CreatedAtRoute("GetParts",PartForReturnDto);
             }

            throw new Exception($"Updating Part {id} failed on save");
        }         
     }
 } 