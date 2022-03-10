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
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        //private readonly IStateRepository _stateRepository;
        private readonly IMapper _mapper;
        public CityService(ICityRepository cityRepository, IMapper mapper) //TODO: inyectar stateRepo
        {
            _cityRepository = cityRepository;
            //_stateRepository = stateRepository;
            _mapper = mapper;
        }
        public async Task<CityResponseDto> AddAsync(CityRequestDto entity)
        {
            var cityTask = _cityRepository.GetFirst(x => x.CityCode == entity.CityCode);
            //var stateTask = _stateRepository.GetFirst(x => x.Id == entity.StateId);
            
            await Task.WhenAll(cityTask); //TODO: añadir stateTask a la espera
            if (cityTask.Result != null) throw new Exception(); //TODO: agregar excepciones personalizadas
            //if (stateTask.Result == null) throw new Exception();

            var newCity = _mapper.Map<CityTable>(entity);

            await _cityRepository.Add(newCity);

            return _mapper.Map<CityResponseDto>(newCity);
        }

        public async Task<CityResponseDto> AddRangeAsync(List<CityRequestDto> entity)
        {
            throw new NotImplementedException();
        }

        public async Task<CityResponseDto> DeleteAsync(int entityId)
        {
            var result = await _cityRepository.GetFirst(x => x.Id == entityId);
            if (result == null) throw new Exception(); //TODO: agregar excepciones personalizadas

            await _cityRepository.Delete(result);

            return _mapper.Map<CityResponseDto>(result);
        }

        public async Task<CityResponseDto> DeleteRangeAsync(List<int> entitiesIds)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CityResponseDto>> GetAllAsync()
        {
            var result = await _cityRepository.GetAll().OrderBy(x => x.CreatedAt).ToListAsync();

            return _mapper.Map<IEnumerable<CityResponseDto>>(result);
        }

        public async Task<CityResponseDto> GetSingleByIdAsync(int entityId)
        {
            var result = await _cityRepository.FindBy(x => x.Id == entityId).Include(y => y.State).FirstOrDefaultAsync();
            if (result == null) throw new Exception(); //TODO: agregar excepciones personalizadas

            return _mapper.Map<CityResponseDto>(result);
        }

        public async Task<CityResponseDto> UpdateAsync(CityRequestDto entity, int entityId)
        {
            var city = await _cityRepository.GetFirst(x => x.Id == entityId);
            if (city == null) throw new Exception();

            city.CityName = entity.CityName;
            city.CityCode = entity.CityCode;
            city.StateId = entity.StateId;
                
            await _cityRepository.Update(city);

            return _mapper.Map<CityResponseDto>(city);
        }

        public async Task<IEnumerable<CityResponseDto>> UpdateRangeAsync(List<CityResponseDto> entity)
        {
            throw new NotImplementedException();
        }
    }
}
