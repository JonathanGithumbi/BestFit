using AutoMapper;
using BestFit.Application.DTOs.RequestDTOs;
using BestFit.Application.Services;
using BestFit.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BestFit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly AuthService authService;

        public AuthController(IMapper mapper, AuthService authService)
        {
            this.mapper = mapper;
            this.authService = authService;
        }

        //POST: /api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            var appUser = new ApplicationUser
            {
                UserName = registerRequestDTO.Username,
                Email = registerRequestDTO.Username,
                Address = registerRequestDTO.Address,
                CellPhone = registerRequestDTO.Phone,
                PostalCode = registerRequestDTO.PostalCode
            };

            var registrationResult = await authService.Register(appUser, registerRequestDTO);

            if (registrationResult == true)
            {
                return Ok("User was registered,please login");
            }
            else
            {
                return BadRequest("Something wet Wrong");
            }
        }


        //POST: /api/Auth/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var loginResponseDTO = await authService.Login(loginRequestDTO);

            return Ok(loginResponseDTO);

        }
    }
}
