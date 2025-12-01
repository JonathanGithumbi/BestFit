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
    public class OrderProductController : ControllerBase
    {
        private readonly OrderProductService orderProductService;
        private readonly IMapper mapper;

        public OrderProductController(OrderProductService orderProductService, IMapper mapper)
        {
            this.orderProductService = orderProductService;
            this.mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<OrderProduct> orderProductList = orderProductService.GetAllOrderProduct();


            return Ok(mapper.Map<List<OrderProductResponseDTO>>(orderProductList));
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var orderProduct = orderProductService.GetOrderProductById(id);


            return Ok(mapper.Map<OrderProductResponseDTO>(orderProduct));
        }

        [HttpPost]
        public IActionResult Post([FromBody] AddOrderProductRequestDTO addOrderProductRequestDTO)
        {
            var orderProductDomainModel = mapper.Map<OrderProduct>(addOrderProductRequestDTO);

            orderProductDomainModel = orderProductService.CreateOrderProduct(orderProductDomainModel);
            var orderProductResponseDTO = mapper.Map<OrderProductResponseDTO>(orderProductDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = orderProductDomainModel.Id }, orderProductResponseDTO);

        }
        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateOrderProductRequestDTO updateOrderProductRequestDTO)
        {
            var orderProductDomainModel = mapper.Map<OrderProduct>(updateOrderProductRequestDTO);

            orderProductDomainModel = orderProductService.UpdateOrderProduct(orderProductDomainModel);

            if (orderProductDomainModel == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(mapper.Map<OrderProductResponseDTO>(orderProductDomainModel));
            }
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var orderdetails = orderProductService.DeleteOrderProduct(id);

            if (orderdetails == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(mapper.Map<OrderProductResponseDTO>(orderdetails));
            }
        }


    }
}
