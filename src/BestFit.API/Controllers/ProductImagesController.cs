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
    public class ProductImageController : ControllerBase
    {
        private readonly ProductImageService productImageService;
        private readonly IMapper mapper;

        public ProductImageController(ProductImageService productImageService, IMapper mapper)
        {
            this.productImageService = productImageService;
            this.mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<ProductImage> productImageList = productImageService.GetAllProductImage();


            return Ok(mapper.Map<List<ProductImageResponseDTO>>(productImageList));
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var productImage = productImageService.GetProductImageById(id);


            return Ok(mapper.Map<ProductImageResponseDTO>(productImage));
        }

        [HttpPost]
        public IActionResult Post([FromForm] AddProductImageRequestDTO addProductImageRequestDTO)
        {
            var productImageDomainModel = mapper.Map<ProductImage>(addProductImageRequestDTO);
            if(productImageService.CreateProductImage(productImageDomainModel))
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateProductImageRequestDTO updateProductImageRequestDTO)
        {
            var productImageDomainModel = mapper.Map<ProductImage>(updateProductImageRequestDTO);

            productImageDomainModel = productImageService.UpdateProductImage(productImageDomainModel);

            if (productImageDomainModel == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(mapper.Map<ProductImageResponseDTO>(productImageDomainModel));
            }
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var product = productImageService.DeleteProductImage(id);

            if (product == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(mapper.Map<ProductImageResponseDTO>(product));
            }
        }


    }
}
