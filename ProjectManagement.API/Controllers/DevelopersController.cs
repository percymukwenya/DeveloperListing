using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ProjectManagement.Common.Models;
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
        private readonly IMemoryCache _memoryCache;
        public DevelopersController(IUnitOfWork unitOfWork, IMemoryCache memoryCache)
        {
            _unitOfWork = unitOfWork;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Developer>>> GetDevelopers()
        {
            var devs = await _unitOfWork.DeveloperRepository.All();

            return Ok(devs);
        }

        [HttpGet("{id}", Name = "GetDeveloper")]
        public async Task<ActionResult<Developer>> GetDeveloper(int id)
        {
            var dev = await _unitOfWork.DeveloperRepository.GetById(id);

            if (dev == null)
                return NotFound();

            return Ok(dev);
        }

        [HttpPost("add-developer")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddDeveloper([FromBody] Developer developer)
        {
            if (developer == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdDev = await _unitOfWork.DeveloperRepository.Upsert(developer);
            if (createdDev != null)
            {
                await _unitOfWork.Completed();
                return CreatedAtRoute(nameof(GetDeveloper), new { Id = createdDev.Id }, createdDev);
            }
            else
            {
                return new JsonResult("Somethign Went wrong") { StatusCode = 500 };
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeveloper(int id)
        {
            var dev = await _unitOfWork.DeveloperRepository.GetById(id);

            if (dev == null)
                return BadRequest();

            var deleted = await _unitOfWork.DeveloperRepository.Delete(id);
            if(deleted)
                await _unitOfWork.Completed();

            return Ok(dev);
        }

        //private async Task<IEnumerable<Developer>> GetDeveloperList()
        //{

        //}
    }
}
