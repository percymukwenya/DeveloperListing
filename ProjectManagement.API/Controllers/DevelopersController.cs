using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ProjectManagement.Common.Helpers;
using ProjectManagement.Common.Extensions;
using ProjectManagement.Common.Models;
using ProjectManagement.Common.Models.DTOs;
using ProjectManagement.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevelopersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DevelopersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeveloperDTO>>> GetDevelopers()
        {
            var devs = await _unitOfWork.DeveloperRepository.All();

            var objDto = new List<DeveloperDTO>();

            foreach(var obj in devs)
            {
                objDto.Add(_mapper.Map<DeveloperDTO>(obj));
            }

            return Ok(objDto);
        }

        [HttpGet("GetByParameters")]
        public async Task<ActionResult<IEnumerable<DeveloperDTO>>> GetByParameters([FromQuery] UserParams userParams)
        {
            var devs = await _unitOfWork.DeveloperRepository.Find(userParams);

            Response.AddPaginationHeader(devs.CurrentPage, devs.PageSize, devs.TotalCount, devs.TotalPages);

            var objDto = new List<DeveloperDTO>();

            foreach (var obj in devs)
            {
                objDto.Add(_mapper.Map<DeveloperDTO>(obj));
            }

            return Ok(objDto);
        }

        [HttpGet("{id}", Name = "GetDeveloper")]
        public async Task<ActionResult<DeveloperDTO>> GetDeveloper(int id)
        {
            var dev = await _unitOfWork.DeveloperRepository.GetById(id);

            if (dev == null)
                return NotFound();

            var objDto = _mapper.Map<DeveloperDTO>(dev);

            return Ok(objDto);
        }

        [HttpPost("add-developer")]
        [Authorize]
        public async Task<IActionResult> AddDeveloper([FromBody] AddDeveloperDTO developerDTO)
        {
            if (developerDTO == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var devObj = _mapper.Map<Developer>(developerDTO);

            var createdDev = await _unitOfWork.DeveloperRepository.Upsert(devObj);
            if (createdDev != null)
            {
                await _unitOfWork.Completed();
                await _unitOfWork.DeveloperRepository.RefreshCache();
                return CreatedAtRoute(nameof(GetDeveloper), new { Id = createdDev.Id }, createdDev);
            }
            else
            {
                return new JsonResult("Something Went wrong") { StatusCode = 500 };
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteDeveloper(int id)
        {
            var dev = await _unitOfWork.DeveloperRepository.GetById(id);

            if (dev == null)
                return BadRequest();

            var deleted = await _unitOfWork.DeveloperRepository.Delete(id);
            if (deleted)
            {
                await _unitOfWork.Completed();
                await _unitOfWork.DeveloperRepository.RefreshCache();
            }                

            return Ok(dev);
        }
    }
}
