using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfServer.Application.DTOs.Requests;
using ProfServer.Application.Interfaces;

namespace ProfServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachineStatusController : ControllerBase
    {
        private readonly IMachineStatusService _machineStatusService;

        public MachineStatusController(IMachineStatusService machineStatusService)
        {
            _machineStatusService = machineStatusService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.MachineStatus>>> GetMachineStatuses()
        {
            var statuses = await _machineStatusService.GetMachineStatusesAsync();
            return Ok(statuses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Models.MachineStatus>> GetMachineStatusById(int id)
        {
            var status = await _machineStatusService.GetMachineStatusByIdAsync(id);
            return Ok(status);
        }

        [HttpPost]
        public async Task<ActionResult<Models.MachineStatus>> CreateMachineStatus([FromBody] CreateMachineStatusRequest request)
        {
            var createdStatus = await _machineStatusService.CreateMachineStatusAsync(request);
            return CreatedAtAction(nameof(GetMachineStatusById), new { id = createdStatus.Id }, createdStatus);
        }

        [HttpPut]
        public async Task<ActionResult<Models.MachineStatus>> UpdateMachineStatus([FromBody] UpdateMachineStatusRequest request)
        {
            var updatedStatus = await _machineStatusService.UpdateMachineStatusAsync(request);
            return Ok(updatedStatus);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMachineStatus(int id)
        {
            var result = await _machineStatusService.DeleteMachineStatusAsync(id);
            return Ok(result);
        }
    }
}
