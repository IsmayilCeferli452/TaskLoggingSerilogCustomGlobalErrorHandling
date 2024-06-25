using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Admin.Countries;
using Service.Services.Interfaces;

namespace TaskApp.Controllers.Admin
{
    public class CountryController : BaseController
    {
        private readonly ICountryService _countryService;
        private readonly ILogger<CountryController> _logger;

        public CountryController(ICountryService countryService, 
                                 ILogger<CountryController> logger)
        {
            _countryService = countryService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Country GetAll method working");
            var countries = await _countryService.GetAllAsync();

            if (countries.Count() < 0)
            {
                throw new ArgumentNullException();
            }
            return Ok(countries);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CountryCreateDto request)
        {
            await _countryService.CreateAsync(request);

            return CreatedAtAction(nameof(Create), new
            {
                Response = "Data succesfully created"
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException();
            }

            var country = await _countryService.GetById((int)id);

            if (country is null)
            {
                throw new ArgumentNullException();
            }

            return Ok(country);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException();
            }
            var country = await _countryService.GetById((int)id);

            if (country is null)
            {
                throw new ArgumentNullException();
            }

            await _countryService.DeleteAsync(country);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromQuery] int? id, [FromBody] CountryEditDto request)
        {
            if (id is null)
            {
                throw new ArgumentNullException();
            }

            var country = await _countryService.GetById((int)id);

            if (country is null)
            {
                throw new ArgumentNullException();
            }

            await _countryService.EditAsync((int)id, request);

            return Ok();
        }
    }
}
