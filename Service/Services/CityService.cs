using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Repository.Repositories.Interfaces;
using Service.DTOs.Admin.Cities;
using Service.Helpers.Exceptions;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class CityService : ICityService
    {
        private readonly IMapper _mapper;
        private readonly ICityRepository _cityRepo;
        private readonly ICountryRepository _countryRepo;
        private readonly ILogger<CityService> _logger;

        public CityService(IMapper mapper,
                           ICityRepository cityRepo,
                           ICountryRepository countryRepo,
                           ILogger<CityService> logger)
        {
            _mapper = mapper;
            _cityRepo = cityRepo;
            _countryRepo = countryRepo;
            _logger = logger;
        }

        public async Task CreateAsync(CityCreateDto request)
        {
            var country = await _countryRepo.GetByIdAsync(request.CountryId);

            if (country is null)
            {
                _logger.LogWarning($"Country not found - {request.CountryId + DateTime.Now.ToString()}");

                throw new NotFoundException($"Id - {request} Country not found");
            }

            await _cityRepo.CreateAsync(_mapper.Map<City>(request));
        }

        public async Task<IEnumerable<CityDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<CityDto>>(await _cityRepo.GetAllWithCountry());
        }

        public async Task<CityDto> GetByIdAsync(int id)
        {
            var city = _cityRepo.FindBy(m => m.Id == id, m => m.Country);

            return _mapper.Map<CityDto>(city.FirstOrDefault());
        }

        public async Task<CityDto> GetByNameAsync(string name)
        {
            return _mapper.Map<CityDto>(_cityRepo.FindBy(m => m.Name == name, m => m.Country).FirstOrDefault());
        }
    }
}
