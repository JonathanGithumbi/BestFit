using BestFit.Domain.Entities;
using BestFit.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestFit.Application.Services
{
    public class CartService
    {
        private readonly IUnitOfWork unitOfWork;

        public CartService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Cart> GetAllCarts()
        {
            var cartList = unitOfWork.CartRepository.GetAll();
            return cartList;
        }

        public Cart CreateCart(Cart cart)
        {
            unitOfWork.CartRepository.Add(cart);
            unitOfWork.Save();
            return cart;
        }
        
        public Cart GetCartById(Guid id)
        {
            var cart = unitOfWork.CartRepository.GetFirstOrDefault(x => x.Id == id);
            return cart;
        }

        public Cart UpdateCart(Cart cart)
        {
            var existingCart = GetCartById(cart.Id);
             
            unitOfWork.CartRepository.Update(cart);
            unitOfWork.Save();
            return cart;
        }

        public bool DeleteCart(Guid id)
        {
            var cart = unitOfWork.CartRepository.GetFirstOrDefault(x => x.Id == id);

            if (cart != null)
            {
                unitOfWork.CartRepository.Remove(cart);
                unitOfWork.Save();
                return true;
            }
            return false;
        }
    }
}
