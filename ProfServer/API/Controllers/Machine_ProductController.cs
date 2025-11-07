using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfServer.Application.DTOs;
using ProfServer.Application.Interfaces;
using ProfServer.Models;

namespace ProfServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Machine_ProductController : ControllerBase
    {
        private readonly IMachine_ProductService _machine_productService;

        public Machine_ProductController(IMachine_ProductService machine_productService)
        {
            _machine_productService = machine_productService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Machine_Product>> GetMachine_ProductById(int id)
        {
            var machine_product = await _machine_productService.GetMachine_ProductByIdAsync(id);
            return Ok(machine_product);
        }

        [HttpGet("machine/{machineId}/product/{productId}")]
        public async Task<ActionResult<Machine_Product>> GetMachine_Product(int machineId, int productId)
        {
            var machine_product = await _machine_productService.GetMachine_ProductByMachineAndProductAsync(machineId, productId);
            return Ok(machine_product);
        }

        [HttpPost]
        public async Task<ActionResult<Machine_Product>> CreateMachine_Product([FromBody] CreateMachine_ProductRequest request)
        {
            var machine_product = await _machine_productService.CreateMachine_ProductAsync(request);
            return CreatedAtAction(nameof(GetMachine_ProductById), new { id = machine_product.Id }, machine_product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteMachine_Product(int id)
        {
            return await _machine_productService.DeleteMachine_ProductAsync(id);
        }
    }
}
