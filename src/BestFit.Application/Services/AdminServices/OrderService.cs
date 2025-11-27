using BestFit.Application.ViewModels;
using BestFit.Domain.Entities;
using BestFit.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestFit.Application.Services.AdminServices
{
    public class OrderService
    {
        private readonly IUnitOfWork unitOfWork;
        public OrderViewModel   OrderViewModel { get; set; }

        public OrderService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<OrderProduct> GetAll()
        {
            var orderlist = unitOfWork.OrderProductRepository.GetAll(x=>x.OrderStatus !="Delivered");
            return orderlist;
        }


        public OrderViewModel Details(Guid id)
        {
            OrderViewModel = new OrderViewModel
            {
                OrderProduct = unitOfWork.OrderProductRepository.GetFirstOrDefault(x => x.Id == id, includeProperties: "AppUser"),
                OrderDetails = unitOfWork.OrderDetailsRepository.GetAll(d => d.OrderProductId == id, includeProperties: "Product")
            };
            return OrderViewModel;
        }


        public OrderViewModel Delivered(OrderViewModel orderViewModel)
        {
            var orderProduct = unitOfWork.OrderProductRepository.GetFirstOrDefault(op=>op.Id == orderViewModel.OrderProduct.Id);

            orderProduct.OrderStatus = "Delivered";

            unitOfWork.OrderProductRepository.Update(orderProduct);
            unitOfWork.Save();

            return orderViewModel;
        }

        public OrderViewModel CancelOrder(OrderViewModel orderVM)
        {
            var orderProduct = unitOfWork.OrderProductRepository.GetFirstOrDefault(op => op.Id == orderVM.OrderProduct.Id);

            orderProduct.OrderStatus = "Cancel";
            unitOfWork.OrderProductRepository.Update(orderProduct);
            unitOfWork.Save();
            return orderVM;

        }
        public OrderViewModel UpdateOrderDetails(OrderViewModel orderViewModel)
        {
            var orderProduct = unitOfWork.OrderProductRepository.GetFirstOrDefault(op=>op.Id==orderViewModel.OrderProduct.Id);

            orderProduct.Address = orderViewModel.OrderProduct.Address;
            orderProduct.PostalCode = orderViewModel.OrderProduct.PostalCode;
            orderProduct.CellPhone = orderViewModel.OrderProduct.CellPhone;
            orderProduct.Name = orderViewModel.OrderProduct.Name;

            unitOfWork.OrderProductRepository.Update(orderProduct);
            unitOfWork.Save();

            return orderViewModel;
        }
    }
}
 