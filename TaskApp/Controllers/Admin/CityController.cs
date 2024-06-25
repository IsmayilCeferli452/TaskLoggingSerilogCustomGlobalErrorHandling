using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Admin.Cities;
using Service.Services.Interfaces;

namespace TaskApp.Controllers.Admin
{
    public class CityController : BaseController
    {
        private readonly ICityService _cityService;
        private readonly ILogger<CityController> _logger;

        public CityController(ICityService cityService, ILogger<CityController> logger)
        {
            _cityService = cityService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("GetAll method worked");

            return Ok(await _cityService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return Ok(await _cityService.GetByIdAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetByName([FromQuery] string name)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return Ok(await _cityService.GetByNameAsync(name));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CityCreateDto request)
        {
            try
            {
                await _cityService.CreateAsync(request);

                return CreatedAtAction(nameof(Create), new { Response = "Data succesfully created" });
            }
            catch(NullReferenceException ex)
            {
                return NotFound();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
