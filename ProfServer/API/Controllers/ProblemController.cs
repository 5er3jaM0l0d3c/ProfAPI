using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfServer.Application.DTOs.Requests;
using ProfServer.Application.Interfaces;
using ProfServer.Models;

namespace ProfServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProblemController : ControllerBase
    {
        private readonly IProblemService _problemService;

        public ProblemController(IProblemService problemService)
        {
            _problemService = problemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Problem>>> GetAllProblems()
        {
            var problems = await _problemService.GetAllProblems();
            return Ok(problems);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Problem>> GetProblemById(int id)
        {
            var problem = await _problemService.GetProblemById(id);
            return Ok(problem);
        }

        [HttpPost]
        public async Task<ActionResult<Problem>> CreateProblem([FromBody] CreateProblemRequest request)
        {
            var problem = await _problemService.CreateProblem(request);
            return CreatedAtAction(nameof(GetProblemById), new { id = problem.Id }, problem);
        }

        [HttpPut]
        public async Task<ActionResult<Problem>> UpdateProblem([FromBody] UpdateProblemRequest request)
        {
            var problem = await _problemService.UpdateProblem(request);
            return Ok(problem);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteProblem(int id)
        {
            return await _problemService.DeleteProblem(id);
        }
    }
}
