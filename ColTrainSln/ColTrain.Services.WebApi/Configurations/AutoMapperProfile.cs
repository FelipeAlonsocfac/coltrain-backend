using AutoMapper;
using ColTrain.Services.DTO;
using ColTrain.Shared.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ColTrain.Services.WebApi.Configurations
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CityTable, CityRequestDto>().ReverseMap();
            CreateMap<CityTable, CityResponseDto>().ReverseMap();

            CreateMap<StateTable, StateRequestDto>().ReverseMap();
            CreateMap<StateTable, StateResponseDto>().ReverseMap();
        }
    }
}
