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
    public class MachineController : ControllerBase
    {
        private readonly IMachineService _machineService;

        public MachineController(IMachineService machineService)
        {
            _machineService = machineService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Machine>>> GetMachines()
        {
            var machines = await _machineService.GetMachinesAsync();
            return Ok(machines);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Machine>> GetMachineById(int id)
        {
            var machine = await _machineService.GetMachineByIdAsync(id);

            return Ok(machine);
        }

        [HttpPost]
        public async Task<ActionResult<MachineDTO>> CreateMachine([FromBody] CreateMachineRequest request)
        {
            var createdMachine = await _machineService.CreateMachineAsync(request);

            return CreatedAtAction(nameof(GetMachineById), new { id = createdMachine.Id }, createdMachine);
        }

        [HttpPut()]
        public async Task<ActionResult<MachineDTO>> UpdateMachine([FromBody] UpdateMachineRequest request)
        {
            var updatedMachine = await _machineService.UpdateMachineAsync(request);
            return Ok(updatedMachine);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMachine(int id)
        {
            var result = await _machineService.DeleteMachineAsync(id);
            return Ok(result);
        }
    }
}
