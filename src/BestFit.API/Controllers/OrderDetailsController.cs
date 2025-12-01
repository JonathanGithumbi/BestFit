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
    public class OrderDetailsController : ControllerBase
    {
        private readonly OrderDetailsService orderDetailsService;
        private readonly IMapper mapper;

        public OrderDetailsController(OrderDetailsService orderDetailsService,IMapper mapper)
        {
            this.orderDetailsService = orderDetailsService;
            this.mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<OrderDetails> orderDetailsList = orderDetailsService.GetAllOrderDetails();

             
            return Ok(mapper.Map<List<OrderDetailsResponseDTO>>(orderDetailsList));
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var orderDetails = orderDetailsService.GetOrderDetailsById(id);


            return Ok(mapper.Map<OrderDetailsResponseDTO>(orderDetails));
        }

        [HttpPost]
        public IActionResult Post([FromBody] AddOrderDetailsRequestDTO addOrderDetailsRequestDTO)
        {
            var orderDetailsDomainModel = mapper.Map<OrderDetails>(addOrderDetailsRequestDTO);

            orderDetailsDomainModel = orderDetailsService.CreateOrderDetails(orderDetailsDomainModel);
            var orderDetailsResponseDTO = mapper.Map<OrderDetailsResponseDTO>(orderDetailsDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = orderDetailsDomainModel.Id }, orderDetailsResponseDTO);

        }
        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateOrderDetailsRequestDTO updateOrderDetailsRequestDTO)
        {
            var orderDetailsDomainModel = mapper.Map<OrderDetails>(updateOrderDetailsRequestDTO);

            orderDetailsDomainModel = orderDetailsService.UpdateOrderDetails(orderDetailsDomainModel);

            if (orderDetailsDomainModel == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(mapper.Map<CategoryResponseDTO>(orderDetailsDomainModel));
            }
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var orderdetails = orderDetailsService.DeleteOrderDetails(id);

            if (orderdetails == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(mapper.Map<OrderDetailsResponseDTO>(orderdetails));
            }
        }


    }
}
