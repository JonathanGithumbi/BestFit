using AutoMapper;
using BestFit.Application.Services;
using BestFit.Shared.DTOs.ResponseDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BestFit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly ProfileService profileService;
        private readonly IMapper mapper;

        public ProfileController(ProfileService profileService,IMapper mapper)
        {
            this.profileService = profileService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> ProfileAccount(string email)
        {

            var profileAccountSR = await profileService.GetProfileAccount(email);
            var profileAccountDTO = mapper.Map<ProfileAccountResponseDTO>(profileAccountSR);

            return Ok(profileAccountDTO);
        }
    }
}
