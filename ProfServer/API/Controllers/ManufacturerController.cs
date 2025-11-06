using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfServer.Application.DTOs.Requests;
using ProfServer.Application.Interfaces;
using ProfServer.Models;

namespace ProfServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturerController : ControllerBase
    {
        private readonly IManufacturerService _manufacturerService;

        public ManufacturerController(IManufacturerService manufacturerService)
        {
            _manufacturerService = manufacturerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Manufacturer>>> GetAllManufacturers()
        {
            var manufacturers = await _manufacturerService.GetAllManufacturersAsync();
            return Ok(manufacturers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Manufacturer>> GetManufacturerById(int id)
        {
            var manufacturer = await _manufacturerService.GetManufacturerByIdAsync(id);
            return Ok(manufacturer);
        }

        [HttpPost]
        public async Task<ActionResult<Manufacturer>> AddManufacturer([FromBody] CreateManufacturerRequest request)
        {
            var manufacturer = await _manufacturerService.AddManufacturerAsync(request);
            return CreatedAtAction(nameof(GetManufacturerById), new { id = manufacturer.Id }, manufacturer);
        }

        [HttpPut]
        public async Task<ActionResult<Manufacturer>> UpdateManufacturer([FromBody] UpdateManufacturerRequest request)
        {
            var manufacturer = await _manufacturerService.UpdateManufacturerAsync(request);
            return Ok(manufacturer);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteManufacturer(int id)
        {
            var result = await _manufacturerService.DeleteManufacturerAsync(id);
            return result;
        }
    }
}
