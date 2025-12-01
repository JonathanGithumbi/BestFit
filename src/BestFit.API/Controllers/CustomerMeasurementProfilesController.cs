using AutoMapper;
using BestFit.Application.DTOs.RequestDTOs;
using BestFit.Application.DTOs.RequestDTOs.AddCustomerMeasurementProfileRequestDTO;
using BestFit.Application.DTOs.RequestDTOs.UpdateCustomerMeasurementProfileRequestDTO;
using BestFit.Application.DTOs.ResponseDTOs;
using BestFit.Application.Services;
using BestFit.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BestFit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerMeasurementProfilesController : ControllerBase
    {
        private readonly CustomerMeasurementProfileService customerMeasurementProfileService;
        private readonly IMapper mapper;

        public CustomerMeasurementProfilesController(CustomerMeasurementProfileService customerMeasurementProfileService, IMapper mapper)
        {
            this.customerMeasurementProfileService = customerMeasurementProfileService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<CustomerMeasurementProfile> profileList = customerMeasurementProfileService.GetAllCustomerMeasurementProfiles();

            var profileListResponseDTO = mapper.Map<List<CustomerMeasurementProfile>>(profileList);
            return Ok(profileList);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var profile = customerMeasurementProfileService.GetCustomerMeasurementProfileById(id);


            return Ok(mapper.Map<CustomerMeasurementProfileResponseDTO>(profile));
        }

        [HttpPost]
        public IActionResult Post([FromBody] AddCustomerMeasurementProfileRequestDTO addCustomerMeasurementProfileRequestDTO)
        {
            var profileDomainModel = mapper.Map<CustomerMeasurementProfile>(addCustomerMeasurementProfileRequestDTO);

            profileDomainModel = customerMeasurementProfileService.CreateCustomerMeasurementProfile(profileDomainModel);
            var profileDTO = mapper.Map<CustomerMeasurementProfileResponseDTO>(profileDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = profileDomainModel.Id }, profileDTO);

        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateCustomerMeasurementProfileRequestDTO updateCustomerMeasurementProfileRequestDTO)
        {
            var profileDomainModel = mapper.Map<CustomerMeasurementProfile>(updateCustomerMeasurementProfileRequestDTO);

            profileDomainModel = customerMeasurementProfileService.UpdateCustomerMeasurementProfile(profileDomainModel);

            if (profileDomainModel == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(mapper.Map<CustomerMeasurementProfileResponseDTO>(profileDomainModel));
            }
        }


        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var profile = customerMeasurementProfileService.DeleteCustomerMeasurementProfile(id);

            if (profile == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(mapper.Map<CustomerMeasurementProfileResponseDTO>(profile));
            }
        }

    }
}
