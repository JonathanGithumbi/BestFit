using BestFit.Domain.Entities;
using BestFit.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestFit.Application.Services
{
    public class OrderProductService
    {
        private readonly IUnitOfWork unitOfWork;

        public OrderProductService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<OrderProduct> GetAllOrderProduct()
        {
            var orderProductList = unitOfWork.OrderProductRepository.GetAll();
            return orderProductList;
        }
        public OrderProduct CreateOrderProduct(OrderProduct orderProduct)
        {
            unitOfWork.OrderProductRepository.Add(orderProduct);
            unitOfWork.Save();
            return orderProduct;
        }
        public OrderProduct GetOrderProductById(Guid id)
        {
            var orderProduct = unitOfWork.OrderProductRepository.GetFirstOrDefault(x => x.Id == id);
            return orderProduct;
        }
        public OrderProduct UpdateOrderProduct(OrderProduct orderProduct)
        {
            var existingOrderProduct = GetOrderProductById(orderProduct.Id);

            unitOfWork.OrderProductRepository.Update(orderProduct);
            unitOfWork.Save();
            return orderProduct;
        }

        public bool DeleteOrderProduct(Guid id)
        {
            var orderProduct = unitOfWork.OrderProductRepository.GetFirstOrDefault(x => x.Id == id);

            if (orderProduct != null)
            {
                unitOfWork.OrderProductRepository.Remove(orderProduct);
                unitOfWork.Save();
                return true;
            }
            return false;
        }
    }
}
