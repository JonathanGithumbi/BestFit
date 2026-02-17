using AutoMapper;
using BestFit.Application.DTOs.ResponseDTOs;
using BestFit.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BestFit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly HomeService homeService;
        private readonly IMapper mapper;

        public HomeController(HomeService homeService,IMapper mapper)
        {
            this.homeService = homeService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetHome()
        {
            var dataDictionary = homeService.Home();

            var homeIndexResponseDTO = new HomeIndexResponseDTO()
            {
                featuredContent = mapper.Map<FeaturedContentResponse>(dataDictionary["featuredContentDomain"]),
                categories = mapper.Map<List<CategoryResponseDTO>>(dataDictionary["categories"]),
                sportCollectionCategories = mapper.Map<List<CategoryResponseDTO>>(dataDictionary["sportCollectionCategories"]),
                popularCategories = mapper.Map<List<CategoryResponseDTO>>(dataDictionary["popularCategories"])
            };
            return Ok(homeIndexResponseDTO);
        }
    }
}
