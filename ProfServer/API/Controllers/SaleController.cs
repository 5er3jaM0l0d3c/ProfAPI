using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfServer.Application.DTOs;
using ProfServer.Application.DTOs.Requests;
using ProfServer.Application.Interfaces;

namespace ProfServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet("machine/{id}")]
        public async Task<ActionResult<IEnumerable<SaleDTO>>> GetSalesFromMachine(int id)
        {
            var sales = await _saleService.GetSalesFromMachineAsync(id);
            return Ok(sales);
        }

        [HttpGet("product/{id}")]
        public async Task<ActionResult<IEnumerable<SaleDTO>>> GetSalesFromProduct(int id)
        {
            var sales = await _saleService.GetSalesWithProductAsync(id);
            return Ok(sales);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SaleDTO>> GetSaleById(int id)
        {
            var sale = await _saleService.GetSaleByIdAsync(id);
            return Ok(sale);
        }

        [HttpPost]
        public async Task<ActionResult<SaleDTO>> CreateSale([FromBody] CreateSaleRequest request)
        {
            var sale = await _saleService.CreateSaleAsync(request);
            return CreatedAtAction(nameof(GetSaleById), new { id = sale.Id }, sale);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteSale(int id)
        {
            return await _saleService.DeleteSaleAsync(id);
        }
    }
}
