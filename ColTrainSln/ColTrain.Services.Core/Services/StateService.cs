using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ColTrain.Services.Contracts.Interfaces.Services;
using ColTrain.Services.DTO;
using ColTrain.Shared.Contracts.Interfaces.Repositories;
using ColTrain.Shared.DTO.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ColTrain.Services.Core.Services
{
    public class StateService : IStateService
    {
        private readonly IStateRepository _stateRepository;
        private readonly IMapper _mapper;
        public StateService(IStateRepository stateRepository, IMapper mapper)
        {
            _stateRepository = stateRepository;
            _mapper = mapper;
        }
        public async Task<StateResponseDto> AddAsync(StateRequestDto entity)
        {
            var stateTask = await _stateRepository.GetFirst(x => x.StateCode == entity.StateCode);
            if (stateTask != null) throw new Exception(); //TODO: agregar excepciones personalizadas

            var newState = _mapper.Map<StateTable>(entity);

            await _stateRepository.Add(newState);

            return _mapper.Map<StateResponseDto>(newState);
        }

        public async Task<StateResponseDto> AddRangeAsync(List<StateRequestDto> entity)
        {
            throw new NotImplementedException();
        }

        public async Task<StateResponseDto> DeleteAsync(int entityId)
        {
            var result = await _stateRepository.GetFirst(x => x.Id == entityId);
            if (result == null) throw new Exception(); //TODO: agregar excepciones personalizadas

            await _stateRepository.Delete(result);

            return _mapper.Map<StateResponseDto>(result);
        }

        public async Task<StateResponseDto> DeleteRangeAsync(List<int> entitiesIds)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<StateResponseDto>> GetAllAsync()
        {
            var result = await _stateRepository.GetAll().OrderBy(x => x.CreatedAt).ToListAsync();

            return _mapper.Map<IEnumerable<StateResponseDto>>(result);
        }

        public async Task<StateResponseDto> GetSingleByIdAsync(int entityId)
        {
            var result = await _stateRepository.GetFirst(x => x.Id == entityId);
            if (result == null) throw new Exception(); //TODO: agregar excepciones personalizadas

            return _mapper.Map<StateResponseDto>(result);
        }

        public async Task<StateResponseDto> UpdateAsync(StateRequestDto entity, int entityId)
        {
            var state = await _stateRepository.GetFirst(x => x.Id == entityId);
            if (state == null) throw new Exception();

            state.StateName = entity.StateName;
            state.StateCode = entity.StateCode;
                
            await _stateRepository.Update(state);

            return _mapper.Map<StateResponseDto>(state);
        }

        public async Task<IEnumerable<StateResponseDto>> UpdateRangeAsync(List<StateResponseDto> entity)
        {
            throw new NotImplementedException();
        }
    }
}
