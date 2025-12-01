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
    public class ProductController : ControllerBase
    {
        private readonly ProductService productService;
        private readonly IMapper mapper;

        public ProductController(ProductService productService, IMapper mapper)
        {
            this.productService = productService;
            this.mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Product> productList = productService.GetAllProduct();


            return Ok(mapper.Map<List<ProductResponseDTO>>(productList));
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var product = productService.GetProductById(id);


            return Ok(mapper.Map<ProductResponseDTO>(product));
        }

        [HttpPost]
        public IActionResult Post([FromBody] AddProductRequestDTO addProductRequestDTO)
        {
            var productDomainModel = mapper.Map<Product>(addProductRequestDTO);

            productDomainModel = productService.CreateProduct(productDomainModel);
            var productResponseDTO = mapper.Map<ProductResponseDTO>(productDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = productDomainModel.Id }, productResponseDTO);

        }
        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateProductRequestDTO updateProductRequestDTO)
        {
            var productDomainModel = mapper.Map<Product>(updateProductRequestDTO);

            productDomainModel = productService.UpdateProduct(productDomainModel);

            if (productDomainModel == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(mapper.Map<ProductResponseDTO>(productDomainModel));
            }
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var product = productService.DeleteProduct(id);

            if (product == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(mapper.Map<ProductResponseDTO>(product));
            }
        }


    }
}
