using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using ColTrain.Services.Contracts.Interfaces.Services;
using ColTrain.Services.DTO;
using ColTrain.Shared.Contracts.Interfaces.Repositories;
using ColTrain.Shared.DTO.Models;
using System.Linq;
using ColTrain.Shared.DTO.Enums;

namespace ColTrain.Services.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<UserResponseDto> SignUpAsync(UserType userType, UserRequestDto oUser)
        {
            var userTask = await _userRepository.GetFirst(x => x.Email == oUser.Email);
            if (userTask != null) throw new Exception(); //TODO: agregar excepciones personalizadas

            oUser.Password = BCrypt.Net.BCrypt.HashPassword(oUser.Password);

            var newUser = _mapper.Map<UserTable>(oUser);
            newUser.UserType = (short)userType;

            await _userRepository.Add(newUser);

            return _mapper.Map<UserResponseDto>(newUser);
        }      

        public async Task<UserResponseDto> LogInAsync(UserLogIn oUser)
        {
            var user = await _userRepository.GetFirst(x => x.Email == oUser.Email);
            if (user == null) return null; //TODO: agregar excepciones personalizadas

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(oUser.Password, user.Password);

            if (isValidPassword) return _mapper.Map<UserResponseDto>(user);

            return null;
        }
    }
}
