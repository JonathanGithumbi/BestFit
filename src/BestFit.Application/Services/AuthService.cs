using BestFit.API.Controllers;
using BestFit.Application.DTOs.RequestDTOs;
using BestFit.Application.DTOs.ResponseDTOs;
using BestFit.Domain.Entities;
using BestFit.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestFit.Application.Services
{
    public class AuthService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthService(UserManager<ApplicationUser> userManager,ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }
           
        public async Task<bool> Register(ApplicationUser user, RegisterRequestDTO registerRequestDTO)
        {
            var appUser = new ApplicationUser
            {
                UserName = registerRequestDTO.Username,
                Email = registerRequestDTO.Username,
                Address = registerRequestDTO.Address,
                CellPhone = registerRequestDTO.Phone,
                PostalCode = registerRequestDTO.PostalCode
            };

            var identityResult = await userManager.CreateAsync(user, registerRequestDTO.Password);

            if(identityResult.Succeeded)
            {
                //Add roles to this user
                if(registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any())
                {
                    identityResult =await userManager.AddToRolesAsync(user, registerRequestDTO.Roles);
                    if(identityResult.Succeeded)
                    {
                        return true;
                    }

                }
                
            }
            return false;


        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {  
            var user = await userManager.FindByEmailAsync(loginRequestDTO.Username);
            var loginResponseDto = new LoginResponseDTO();
            loginResponseDto.Message = "Login Failed";
            
            if (user !=null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDTO.Password);
                if(checkPasswordResult == true)
                {
                    var roles = await userManager.GetRolesAsync(user);
                    if(roles != null)
                    {
                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());
                        loginResponseDto.jwtToken = jwtToken;
                        loginResponseDto.Message = "Login Succsessfull";
                    }
                    
                }
            }

            return loginResponseDto;
        }
    }
}
