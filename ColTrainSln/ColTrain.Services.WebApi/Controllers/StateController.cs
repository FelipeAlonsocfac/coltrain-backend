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
    public class StateController : ControllerBase
    {
        private readonly IStateService _stateService;

        public StateController(IStateService stateService)
        {
            _stateService = stateService;
        }

        /// <summary>
        /// This method returns all available states in database.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<StateResponseDto>), 200)]
        [ProducesResponseType(typeof(FailedOperationResult), 404)]
        [ProducesResponseType(typeof(FailedOperationResult), 400)]
        public async Task<IActionResult> GetCities()
        {
            var states = await _stateService.GetAllAsync();
            return Ok(states);
        }

        /// <summary>
        /// This method returns a specific state according to the id sent as a parameter.
        /// </summary>
        /// <param name="stateId"></param>
        /// <returns></returns>
        [HttpGet("{stateId}")]
        [ProducesResponseType(typeof(StateResponseDto), 200)]
        [ProducesResponseType(typeof(FailedOperationResult), 404)]
        [ProducesResponseType(typeof(FailedOperationResult), 400)]
        public async Task<IActionResult> GetStateById(int stateId)
        {
            var state = await _stateService.GetSingleByIdAsync(stateId);
            return Ok(state);
        }

        /// <summary>
        /// This method creates a new state.
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(StateResponseDto), 201)]
        [ProducesResponseType(typeof(StateResponseDto), 200)]
        [ProducesResponseType(typeof(FailedOperationResult), 400)]
        public async Task<IActionResult> AddState(StateRequestDto state)
        {
            var result = await _stateService.AddAsync(state);
            return Ok(result);
        }

        /// <summary>
        /// This method updates a specific state that matches the id sent as a parameter in database.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="stateId"></param>
        /// <returns></returns>
        [HttpPut("{stateId}")]
        [ProducesResponseType(typeof(StateResponseDto), 200)]
        [ProducesResponseType(typeof(FailedOperationResult), 404)]
        [ProducesResponseType(typeof(FailedOperationResult), 400)]
        public async Task<IActionResult> UpdateState(StateRequestDto state, int stateId)
        {
            var result = await _stateService.UpdateAsync(state, stateId);
            return Ok(result);
        }

        /// <summary>
        /// This method deletes a specific state that matches the id sent as a parameter in database.
        /// </summary>
        /// <param name="stateId"></param>
        /// <returns></returns>
        [HttpDelete("{stateId}")]
        [ProducesResponseType(typeof(StateResponseDto), 200)]
        [ProducesResponseType(typeof(FailedOperationResult), 404)]
        [ProducesResponseType(typeof(FailedOperationResult), 400)]
        public async Task<IActionResult> DeleteTimezone(int stateId)
        {
            var state = await _stateService.DeleteAsync(stateId);
            return Ok(state);
        }
    }
}
