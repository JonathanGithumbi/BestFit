using AutoMapper;
using BestFit.API.Controllers;
using BestFit.Application.DTOs.RequestDTOs;

using BestFit.Application.DTOs.ResponseDTOs;
using BestFit.Application.DTOs.RequestDTOs.UpdateCustomerMeasurementProfileRequestDTO;
using BestFit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace BestFit.Application.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            //Category
            CreateMap<Category, CategoryResponseDTO>().ReverseMap();
            CreateMap<Category, AddCategoryRequestDTO>().ReverseMap();
            CreateMap<Category, UpdateCategoryRequestDTO>().ReverseMap();

            //Cart
            CreateMap<Cart, CartResponseDTO>().ReverseMap();
            CreateMap<Cart, AddCartRequestDTO>().ReverseMap();
            CreateMap<Cart, UpdateCartRequestDTO>().ReverseMap();


            //CustomerMeasurementProfile
            CreateMap<CustomerMeasurementProfile, CustomerMeasurementProfileResponseDTO>().ReverseMap();
            CreateMap<CustomerMeasurementProfile, AddCustomerMeasurementProfileRequestDTO>().ReverseMap();
            CreateMap<CustomerMeasurementProfile, UpdateCustomerMeasurementProfileRequestDTO>().ReverseMap();


            //OrderDetails
            CreateMap<OrderDetails, OrderDetailsResponseDTO>().ReverseMap();
            CreateMap<OrderDetails, AddOrderDetailsRequestDTO>().ReverseMap();
            CreateMap<OrderDetails, UpdateOrderDetailsRequestDTO>().ReverseMap();

            //OrderProduct
            CreateMap<OrderProduct, OrderProductResponseDTO>().ReverseMap();
            CreateMap<OrderProduct, AddOrderProductRequestDTO>().ReverseMap();
            CreateMap<OrderProduct, UpdateOrderProductRequestDTO>().ReverseMap();


            //Product
            CreateMap<Product, ProductResponseDTO>().ReverseMap();
            CreateMap<Product, AddProductRequestDTO>().ReverseMap();
            CreateMap<Product, UpdateProductRequestDTO>().ReverseMap();

            //ProductImage
            CreateMap<ProductImage, ProductImageResponseDTO>().ReverseMap();
            CreateMap<ProductImage, AddProductImageRequestDTO>().ReverseMap();
            CreateMap<ProductImage, UpdateProductImageRequestDTO>().ReverseMap();

            //ProductMeasurementProfile
            CreateMap<ProductMeasurementProfile, ProductMeasurementProfileResponseDTO>().ReverseMap();
            CreateMap<ProductMeasurementProfile, AddProductMeasurementProfileRequestDTO>().ReverseMap();
            CreateMap<ProductMeasurementProfile, UpdateProductMeasurementProfileRequestDTO>().ReverseMap();


            //ApplicationUser
            //CreateMap<ApplicationUser, RegisterRequestDTO>().ReverseMap();
      
        }
    }
}
