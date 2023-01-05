using ColTrain.Services.DTO;
using ColTrain.Shared.DTO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColTrain.Services.Contracts.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserResponseDto> SignUpAsync(UserType userType, UserRequestDto oUser);
        Task<UserResponseDto> LogInAsync(UserLogIn oUser);
    }
}
