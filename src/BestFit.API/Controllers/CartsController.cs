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
    public class CartsController : ControllerBase
    {
        private readonly CartService cartService;
        private readonly IMapper mapper;

        public CartsController(CartService cartService,IMapper mapper)
        {
            this.cartService = cartService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var cartList = cartService.GetAllCarts();

            var cartListResponseDTO = mapper.Map<List<Cart>>(cartList);
            return Ok(cartList);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var cart = cartService.GetCartById(id);


            return Ok(mapper.Map<CartResponseDTO>(cart));
        }

        [HttpPost]
        public IActionResult Post([FromBody] AddCartRequestDTO addCartRequestDTO)
        {
            var cartDomainModel = mapper.Map<Cart>(addCartRequestDTO);

            cartDomainModel = cartService.CreateCart(cartDomainModel);
            var cartDTO = mapper.Map<CartResponseDTO>(cartDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = cartDomainModel.Id }, cartDTO);

        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateCartRequestDTO updateCartRequestDTO)
        {
            var cartDomainModel = mapper.Map<Cart>(updateCartRequestDTO);

            cartDomainModel = cartService.UpdateCart(cartDomainModel);

            if (cartDomainModel == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(mapper.Map<CartResponseDTO>(cartDomainModel));
            }
        }


        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var cart = cartService.DeleteCart(id);

            if (cart == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(mapper.Map<CartResponseDTO>(cart));
            }
        }
    }
}
