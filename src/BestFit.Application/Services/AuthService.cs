using BestFit.Shared.DTOs.RequestDTOs;
using BestFit.Shared.DTOs.ResponseDTOs;
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
        private readonly SignInManager<ApplicationUser> signInManager;

        public AuthService(UserManager<ApplicationUser> userManager,ITokenRepository tokenRepository,SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
            this.signInManager = signInManager;
        }
           
        public async Task<RegisterResponseDTO> Register(ApplicationUser user, RegisterRequestDTO registerRequestDTO)
        {
            var registerResponseDTO = new RegisterResponseDTO();
            var identityResult = await userManager.CreateAsync(user, registerRequestDTO.Password);

            if (identityResult.Succeeded)
            {
                var roleResult = await userManager.AddToRolesAsync(user, ["Shopper", "Administrator"]);
                if (roleResult.Succeeded)
                {
                    registerResponseDTO.Message = "Registered succsessfully!";
                    registerResponseDTO.Succeeded = identityResult.Succeeded;
                    return registerResponseDTO;
                }else
                {
                    registerResponseDTO.Errors = roleResult.Errors;
                    registerResponseDTO.Succeeded = roleResult.Succeeded;
                    registerResponseDTO.Message = "Registration interrupted, please try again";
                    return registerResponseDTO;
                    
                }

            }
            else
            {
                registerResponseDTO.Succeeded = identityResult.Succeeded;
                registerResponseDTO.Errors = identityResult.Errors.ToArray();
                registerResponseDTO.Message = "Registration Failed, please try again";
            }
                return registerResponseDTO;


        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {  
            var user = await userManager.FindByEmailAsync(loginRequestDTO.Email);
            var loginResponseDto = new LoginResponseDTO();
            loginResponseDto.Message = "Incorrect username or password";
            
            if (user !=null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDTO.Password);
                if(checkPasswordResult == true)
                {
                    var signInResult = signInManager.PasswordSignInAsync(loginRequestDTO.Email, loginRequestDTO.Password, loginRequestDTO.RememberMe, false);

                    var roles = await userManager.GetRolesAsync(user);
                    if(roles != null)
                    {
                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());
                        loginResponseDto.jwtToken = jwtToken;
                        loginResponseDto.Message = "Login Succsessfull";
                        loginResponseDto.signInResult = await signInResult;
                    }
                    
                }
            }

            return loginResponseDto;
        }

        public Task LogoutAsync()
            => signInManager.SignOutAsync();
        
    }
}
