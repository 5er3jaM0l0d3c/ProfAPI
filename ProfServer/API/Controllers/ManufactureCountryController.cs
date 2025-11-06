using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfServer.Application.DTOs.Requests;
using ProfServer.Application.Interfaces;
using ProfServer.Models;

namespace ProfServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufactureCountryController : ControllerBase
    {
        private readonly IManufactureCountryService _manufactureCountryService;
        public ManufactureCountryController(IManufactureCountryService manufactureCountryService)
        {
            _manufactureCountryService = manufactureCountryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ManufactureCountry>>> GetManufactureCountries()
        {
            var countries = await _manufactureCountryService.GetManufactureCountriesAsync();
            return Ok(countries);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ManufactureCountry>> GetManufactureCountry(int id)
        {
            var country = await _manufactureCountryService.GetManufactureCountryByIdAsync(id);
            return Ok(country);
        }

        [HttpPost]
        public async Task<ActionResult<ManufactureCountry>> CreateManufactureCountry([FromBody] CreateManufactureCountryRequest request)
        {
            var country = await _manufactureCountryService.CreateManufactureCountryAsync(request);
            return CreatedAtAction(nameof(GetManufactureCountry), new { id = country.Id }, country);
        }

        [HttpPut]
        public async Task<ActionResult<ManufactureCountry>> UpdateManufactureCountry([FromBody] UpdateManufactureCountryRequest request)
        {
            var country = await _manufactureCountryService.UpdateManufactureCountryAsync(request);
            return Ok(country);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteManufactureCountry(int id)
        {
            return await _manufactureCountryService.DeleteManufactureCountryAsync(id);
        }
    }
}
