using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfServer.Application.DTOs.Requests;
using ProfServer.Application.Interfaces;
using ProfServer.Models;

namespace ProfServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceController : ControllerBase
    {
        private readonly IMaintenanceService _maintenanceService;

        public MaintenanceController(IMaintenanceService maintenanceService)
        {
            _maintenanceService = maintenanceService;
        }

        [HttpGet("ByMachine/{id}")]
        public async Task<ActionResult<IEnumerable<Maintenance>>> GetMaintenancesByMachineId(int id)
        {
            var maintenances = await _maintenanceService.GetMaintenancesByMachineId(id);
            return Ok(maintenances);
        }

        [HttpGet("ByUser/{id}")]
        public async Task<ActionResult<IEnumerable<Maintenance>>> GetMaintenancesByUserId(int id)
        {
            var maintenances = await _maintenanceService.GetMaintenancesByUserId(id);
            return Ok(maintenances);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Maintenance>> GetMaintenanceById(int id)
        {
            var maintenance = await _maintenanceService.GetMaintenanceById(id);
            return Ok(maintenance);
        }

        [HttpPost]
        public async Task<ActionResult<Maintenance>> CreateMaintenance([FromBody] CreateMaintenanceRequest request)
        {
            var maintenance = await _maintenanceService.CreateMaintenance(request);
            return CreatedAtAction(nameof(GetMaintenanceById), new { id = maintenance.Id }, maintenance);
        }

        [HttpPut]
        public async Task<ActionResult<Maintenance>> UpdateMaintenance([FromBody] UpdateMaintenanceRequest request)
        {
            var maintenance = await _maintenanceService.UpdateMaintenance(request);
            return Ok(maintenance);
        }

        /*[HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteMaintenance(int id)
        {
            return await _maintenanceService.DeleteMaintenance(id);
        }*/
    }
}
