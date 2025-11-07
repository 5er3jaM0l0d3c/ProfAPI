using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfServer.Application.DTOs;
using ProfServer.Application.DTOs.Requests;
using ProfServer.Application.Interfaces;
using ProfServer.Models;

namespace ProfServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Maintenance_WorkDescriptionController : ControllerBase
    {
        private readonly IMaintenance_WorkDescriptionService _maintanence_WorkDesctiprionService;

        public Maintenance_WorkDescriptionController(IMaintenance_WorkDescriptionService maintanence_WorkDesctiprionService)
        {
            _maintanence_WorkDesctiprionService = maintanence_WorkDesctiprionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Maintenance_WorkDescription>>> GetAll()
        {
            var result = await _maintanence_WorkDesctiprionService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Maintenance_WorkDescription>> GetById(int id)
        {
            var result = await _maintanence_WorkDesctiprionService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Maintenance_WorkDescription>> Create([FromBody] CreateMaintenance_WorkDescriptionRequest request)
        {
            var result = await _maintanence_WorkDesctiprionService.AddAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut]
        public async Task<ActionResult<Maintenance_WorkDescription>> Update([FromBody] UpdateMaintenance_WorkDescriptionRequest request)
        {
            var result = await _maintanence_WorkDesctiprionService.UpdateAsync(request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return await _maintanence_WorkDesctiprionService.DeleteAsync(id);
        }
    }
}
