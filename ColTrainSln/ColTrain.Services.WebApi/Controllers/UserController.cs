using ColTrain.Services.Contracts.Interfaces.Services;
using ColTrain.Services.DTO;
using ColTrain.Shared.DTO;
using ColTrain.Shared.DTO.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ColTrain.Services.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public UserController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        /// <summary>
        /// This method Registers a new user in coltrain db
        /// </summary>
        /// ///// <param name="model"></param>
        /// ///// <param name="userType"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [ProducesResponseType(typeof(AuthenticationResponse), 200)]
        [ProducesResponseType(typeof(FailedOperationResult), 404)]
        [ProducesResponseType(typeof(FailedOperationResult), 400)]
        public async Task<IActionResult> SignUp(UserType userType, [FromBody] UserRequestDto model)
        {
            var roleType = User.FindFirstValue(ClaimTypes.Role);
            if (roleType != "Admin") userType = UserType.User;

            var result = await _userService.SignUpAsync(userType, model);
            if (result == null) return Conflict();

            return BuildToken(result);
        }

        /// <summary>
        /// This method tries to log in a user in coltrain db
        /// </summary>
        /// ///// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthenticationResponse), 200)]
        [ProducesResponseType(typeof(FailedOperationResult), 404)]
        [ProducesResponseType(typeof(FailedOperationResult), 400)]
        public async Task<IActionResult> LogIn([FromBody] UserLogIn user)
        {
            var result = await _userService.LogInAsync(user);
            if (result == null) return Unauthorized();

            return BuildToken(result);
        }

        /// <summary>
        /// This method Builds a jwt token
        /// </summary>
        /// <returns></returns>
        private IActionResult BuildToken(UserResponseDto user) //TODO: wrap it into token service
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
                new Claim(ClaimTypes.Role, user.UserType.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())//token id
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretId"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(1);

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: "yourdomain.com",
               audience: "yourdomain.com",
               claims: claims,
               expires: expiration,
               signingCredentials: creds);

            return Ok(new AuthenticationResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpirationDate = expiration
            });

        }
    }
}
