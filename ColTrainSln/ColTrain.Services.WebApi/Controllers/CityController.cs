using ColTrain.Services.Contracts.Interfaces.Services;
using ColTrain.Services.DTO;
using ColTrain.Shared.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ColTrain.Services.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        /// <summary>
        /// This method returns all available cities in database.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CityResponseDto>), 200)]
        [ProducesResponseType(typeof(FailedOperationResult), 404)]
        [ProducesResponseType(typeof(FailedOperationResult), 400)]
        public async Task<IActionResult> GetCities()
        {
            var cities = await _cityService.GetAllAsync();
            return Ok(cities);
        }

        /// <summary>
        /// This method returns a specific city according to the id sent as a parameter.
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        [HttpGet("{cityId}")]
        [ProducesResponseType(typeof(CityResponseDto), 200)]
        [ProducesResponseType(typeof(FailedOperationResult), 404)]
        [ProducesResponseType(typeof(FailedOperationResult), 400)]
        public async Task<IActionResult> GetCityById(int cityId)
        {
            var city = await _cityService.GetSingleByIdAsync(cityId);
            return Ok(city);
        }

        /// <summary>
        /// This method creates a new city.
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(CityResponseDto), 201)]
        [ProducesResponseType(typeof(CityResponseDto), 200)]
        [ProducesResponseType(typeof(FailedOperationResult), 400)]
        public async Task<IActionResult> AddCity(CityRequestDto city)
        {
            var result = await _cityService.AddAsync(city);
            return Ok(result);
        }

        /// <summary>
        /// This method updates a specific city that matches the id sent as a parameter in database.
        /// </summary>
        /// <param name="city"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        [HttpPut("{cityId}")]
        [ProducesResponseType(typeof(CityResponseDto), 200)]
        [ProducesResponseType(typeof(FailedOperationResult), 404)]
        [ProducesResponseType(typeof(FailedOperationResult), 400)]
        public async Task<IActionResult> UpdateCity(CityRequestDto city, int cityId)
        {
            var result = await _cityService.UpdateAsync(city, cityId);
            return Ok(result);
        }

        /// <summary>
        /// This method deletes a specific city that matches the id sent as a parameter in database.
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        [HttpDelete("{cityId}")]
        [ProducesResponseType(typeof(CityResponseDto), 200)]
        [ProducesResponseType(typeof(FailedOperationResult), 404)]
        [ProducesResponseType(typeof(FailedOperationResult), 400)]
        public async Task<IActionResult> DeleteTimezone(int cityId)
        {
            var city = await _cityService.DeleteAsync(cityId);
            return Ok(city);
        }
    }
}
