using AutoMapper;
using BestFit.API.Controllers;
using BestFit.Application.DTOs.RequestDTOs;
using BestFit.Application.DTOs.RequestDTOs.AddCustomerMeasurementProfileRequestDTO;
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
        }
    }
}
