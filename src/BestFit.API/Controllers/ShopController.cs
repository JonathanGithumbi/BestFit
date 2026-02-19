using AutoMapper;
using BestFit.Application.DTOs.ResponseDTOs;
using BestFit.Application.Services;
using BestFit.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BestFit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ProductService productService;
        private readonly IMapper mapper;

        public ShopController(IUnitOfWork unitOfWork, ProductService productService,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.productService = productService;
            this.mapper = mapper;
        }
        [HttpGet]
        public IActionResult Index([FromQuery] string?filterOn,[FromQuery]string? filterQuery
            , [FromQuery] string? sortBy, [FromQuery] bool isAscending = true, [FromQuery] int pageNumber =1, [FromQuery] int pageSize = 10)
        {
            var productsDomain = productService.GetAllProduct(filterOn, filterQuery,sortBy,isAscending,pageNumber,pageSize);

            var responseDTO = new ShopIndexResponseDTO
            {
                productsResponseDTOs = mapper.Map<List<ProductResponseDTO>>(productsDomain)
            };

            return Ok(responseDTO);
        }
    }
}
