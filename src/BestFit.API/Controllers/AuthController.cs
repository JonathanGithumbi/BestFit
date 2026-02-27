using AutoMapper;
using BestFit.Shared.DTOs.RequestDTOs;
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
                UserName = registerRequestDTO.Email,
                FirstName = registerRequestDTO.FirstName,
                LastName = registerRequestDTO.LastName,
                Email = registerRequestDTO.Email,
            };

            var registerResponseDTO = await authService.Register(appUser, registerRequestDTO);

            return Ok(registerResponseDTO);
        }


        //POST: /api/Auth/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var loginResponseDTO = await authService.Login(loginRequestDTO);

            return Ok(loginResponseDTO);

        }

        [HttpGet]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await authService.LogoutAsync();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
            

        }


    }
}
