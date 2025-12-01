using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BestFit.Application.DTOs.RequestDTOs;
using BestFit.Application.DTOs.ResponseDTOs;
using BestFit.Domain.Entities;



namespace BestFit.Application.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<Category, CategoryResponseDTO>().ReverseMap();
            CreateMap<Category, AddCategoryRequestDTO>().ReverseMap();
            CreateMap<Category, UpdateCategoryRequestDTO>().ReverseMap();
        }
    }
}
