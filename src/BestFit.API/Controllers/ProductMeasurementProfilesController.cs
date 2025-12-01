using AutoMapper;
using BestFit.Application.DTOs.RequestDTOs;
using BestFit.Application.DTOs.ResponseDTOs;
using BestFit.Application.Services;
using BestFit.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BestFit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductMeasurementProfileController : ControllerBase
    {
        private readonly ProductMeasurementProfileService productMeasurementProfileService;
        private readonly IMapper mapper;

        public ProductMeasurementProfileController(ProductMeasurementProfileService productMeasurementProfileService, IMapper mapper)
        {
            this.productMeasurementProfileService = productMeasurementProfileService;
            this.mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<ProductMeasurementProfile> productMeasurementProfileList = productMeasurementProfileService.GetAllProductMeasurementProfile();


            return Ok(mapper.Map<List<ProductMeasurementProfileResponseDTO>>(productMeasurementProfileList));
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var productMeasurementProfile = productMeasurementProfileService.GetProductMeasurementProfileById(id);


            return Ok(mapper.Map<ProductMeasurementProfileResponseDTO>(productMeasurementProfile));
        }

        [HttpPost]
        public IActionResult Post([FromBody] AddProductMeasurementProfileRequestDTO addProductMeasurementProfileRequestDTO)
        {
            var productMeasurementProfileDomainModel = mapper.Map<ProductMeasurementProfile>(addProductMeasurementProfileRequestDTO);

            productMeasurementProfileDomainModel = productMeasurementProfileService.CreateProductMeasurementProfile(productMeasurementProfileDomainModel);
            var productMeasurementProfileResponseDTO = mapper.Map<ProductMeasurementProfileResponseDTO>(productMeasurementProfileDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = productMeasurementProfileDomainModel.Id }, productMeasurementProfileResponseDTO);

        }
        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateProductMeasurementProfileRequestDTO updateProductMeasurementProfileRequestDTO)
        {
            var productMeasurementProfileDomainModel = mapper.Map<ProductMeasurementProfile>(updateProductMeasurementProfileRequestDTO);

            productMeasurementProfileDomainModel = productMeasurementProfileService.UpdateProductMeasurementProfile(productMeasurementProfileDomainModel);

            if (productMeasurementProfileDomainModel == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(mapper.Map<ProductMeasurementProfileResponseDTO>(productMeasurementProfileDomainModel));
            }
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var productMeasurementProfile = productMeasurementProfileService.DeleteProductMeasurementProfile(id);

            if (productMeasurementProfile == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(mapper.Map<ProductMeasurementProfileResponseDTO>(productMeasurementProfile));
            }
        }


    }
}
