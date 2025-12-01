using BestFit.Domain.Entities;
using BestFit.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestFit.Application.Services
{
    public class OrderDetailsService
    {
        private readonly IUnitOfWork unitOfWork;

        public OrderDetailsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<OrderDetails> GetAllOrderDetails()
        {
            var orderdetailsList = unitOfWork.OrderDetailsRepository.GetAll();
            return orderdetailsList;
        }
        public OrderDetails CreateOrderDetails(OrderDetails orderDetails )
        {
            unitOfWork.OrderDetailsRepository.Add(orderDetails);
            unitOfWork.Save();
            return orderDetails;
        }
        public OrderDetails GetOrderDetailsById(Guid id)
        {
            var orderDetails = unitOfWork.OrderDetailsRepository.GetFirstOrDefault(x => x.Id == id);
            return orderDetails;
        }
        public OrderDetails UpdateOrderDetails(OrderDetails orderDetails)
        {
            var existingOrderDetails = GetOrderDetailsById(orderDetails.Id);

            unitOfWork.OrderDetailsRepository.Update(existingOrderDetails);
            unitOfWork.Save();
            return existingOrderDetails;
        }

        public bool DeleteOrderDetails(Guid id)
        {
            var orderDetails = unitOfWork.OrderDetailsRepository.GetFirstOrDefault(x => x.Id == id);

            if (orderDetails != null)
            {
                unitOfWork.OrderDetailsRepository.Remove(orderDetails);
                unitOfWork.Save();
                return true;
            }
            return false;
        }
    }
}
