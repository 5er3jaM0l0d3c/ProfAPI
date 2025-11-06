using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfServer.Application.DTOs.Requests;
using ProfServer.Application.Interfaces;
using ProfServer.Models;

namespace ProfServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentTypeController : ControllerBase
    {
        private readonly IPaymentTypeService _paymentTypeService;

        public PaymentTypeController(IPaymentTypeService paymentTypeService)
        {
            _paymentTypeService = paymentTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentType>>> GetPaymentTypes()
        {
            var paymentTypes = await _paymentTypeService.GetPaymentTypesAsync();
            return Ok(paymentTypes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentType>> GetPaymentTypeById(int id)
        {
            var paymentType = await _paymentTypeService.GetPaymentTypeByIdAsync(id);
            return Ok(paymentType);
        }

        [HttpPost]
        public async Task<ActionResult<PaymentType>> CreatePaymentType([FromBody] CreatePaymentTypeRequest request)
        {
            var createdPaymentType = await _paymentTypeService.CreatePaymentTypeAsync(request);
            return CreatedAtAction(nameof(GetPaymentTypeById), new { id = createdPaymentType.Id }, createdPaymentType);
        }

        [HttpPut]
        public async Task<ActionResult<PaymentType>> UpdatePaymentType([FromBody] UpdatePaymentTypeRequest request)
        {
            var updatedPaymentType = await _paymentTypeService.UpdatePaymentTypeAsync(request);
            return Ok(updatedPaymentType);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentType(int id)
        {
            var result = await _paymentTypeService.DeletePaymentTypeAsync(id);
            return Ok(result);
        }
    }
}
