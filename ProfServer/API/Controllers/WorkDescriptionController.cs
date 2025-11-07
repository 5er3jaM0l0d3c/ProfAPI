using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfServer.Application.DTOs.Requests;
using ProfServer.Application.Interfaces;
using ProfServer.Models;

namespace ProfServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkDescriptionController : ControllerBase
    {
        private readonly IWorkDescriptionService _workDescriptionService;

        public WorkDescriptionController(IWorkDescriptionService workDescriptionService)
        {
            _workDescriptionService = workDescriptionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkDescription>>> GetAllWorkDescriptions()
        {
            var workDescriptions = await _workDescriptionService.GetAllWorkDescriptions();
            return Ok(workDescriptions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkDescription>> GetWorkDescriptionById(int id)
        {
            var workDescription = await _workDescriptionService.GetWorkDescriptionById(id);
            return Ok(workDescription);
        }

        [HttpPost]
        public async Task<ActionResult<WorkDescription>> CreateWorkDescription([FromBody] CreateWorkDescriptionRequest workDescription)
        {
            var createdWorkDescription = await _workDescriptionService.CreateWorkDescription(new Application.DTOs.Requests.CreateWorkDescriptionRequest { Name = workDescription.Name });
            return CreatedAtAction(nameof(GetWorkDescriptionById), new { id = createdWorkDescription.Id }, createdWorkDescription);
        }

        [HttpPut]
        public async Task<ActionResult<WorkDescription>> UpdateWorkDescription([FromBody] UpdateWorkDescriptionRequest request)
        {
            var updatedWorkDescription = await _workDescriptionService.UpdateWorkDescription(request);
            return Ok(updatedWorkDescription);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteWorkDescription(int id)
        {
            return await _workDescriptionService.DeleteWorkDescription(id);
        }
    }
}
