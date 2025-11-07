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
    public class Maintenance_ProblemController : ControllerBase
    {
        private readonly IMaintenance_ProblemService _maintenanceProblemService;

        public Maintenance_ProblemController(IMaintenance_ProblemService maintenanceProblemService)
        {
            _maintenanceProblemService = maintenanceProblemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Maintenance_Problem>>> GetAllProblems()
        {
            var problems = await _maintenanceProblemService.GetAllProblemsAsync();
            return Ok(problems);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Maintenance_Problem>> GetProblemById(int id)
        {
            var problem = await _maintenanceProblemService.GetProblemByIdAsync(id);
            return Ok(problem);
        }

        [HttpPost]
        public async Task<ActionResult<Maintenance_Problem>> CreateProblem([FromBody] CreateMaintenance_ProblemRequest request)
        {
            var createdProblem = await _maintenanceProblemService.CreateProblemAsync(request);
            return CreatedAtAction(nameof(GetProblemById), new { id = createdProblem.Id }, createdProblem);
        }

        [HttpPut]
        public async Task<ActionResult<Maintenance_Problem>> UpdateProblem([FromBody] UpdateMaintenance_ProblemRequest request)
        {
            var updatedProblem = await _maintenanceProblemService.UpdateProblemAsync(request);
            return Ok(updatedProblem);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteProblem(int id)
        {
            return await _maintenanceProblemService.DeleteProblemAsync(id);
        }
    }
}
