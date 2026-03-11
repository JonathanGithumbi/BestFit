using AutoMapper;
using BestFit.Domain.Entities;
using BestFit.Shared.DTOs.RequestDTOs;
using BestFit.Shared.DTOs.ResponseDTOs;
using BestFit.Shared.DTOs.ServiceResponse;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestFit.Application.Services
{
    public class ProfileService
    {
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly CustomerMeasurementProfileService profileService;

        public ProfileService(IMapper mapper,UserManager<ApplicationUser> userManager,CustomerMeasurementProfileService profileService)
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.profileService = profileService;
        }
        public async Task<ProductAccountServiceResponse> GetProfileAccount(string email)
        {
            var profileAccountServiceResponse = new ProductAccountServiceResponse();
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return null;
            }

            profileAccountServiceResponse.FirstName = user.FirstName;
            profileAccountServiceResponse.LastName = user.LastName;
            var profiles = profileService.GetAllCustomerMeasurementProfiles(x => x.CustomerId == user.Id);
            profileAccountServiceResponse.CustomerMeasurementProfiles = profiles;
            return profileAccountServiceResponse;

        }
    }
}
