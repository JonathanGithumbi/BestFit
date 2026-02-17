using AutoMapper;
using BestFit.Application.DTOs.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BestFit.Application.DTOs.ResponseDTOs;
using BestFit.Domain.Entities;

namespace BestFit.Application.Services
{
    public class HomeService
    {
        private readonly FeaturedContentService featuredContentService;
        private readonly IMapper mapper;
        private readonly CategoryService categoryService;

        public HomeService(FeaturedContentService featuredContentService,IMapper mapper,CategoryService categoryService)
        {
            this.featuredContentService = featuredContentService;
            this.mapper = mapper;
            this.categoryService = categoryService;
        }
        public Dictionary<string,object> Home()
        {
            var dataDictionary = new Dictionary<string,object>();
            //here we build the data model for the index page
            var indexPageDTO = new HomeIndexResponseDTO();


            //Todays Featured Content
            var today = DateTime.Now;
            var contentDomain = featuredContentService.GetTodayContent(today.Date);
            dataDictionary.Add("featuredContentDomain",contentDomain);

            //Categories Mens Kids & Womens
            var categories = new List<Category>();
            var men = categoryService.GetSingleCategory(x => x.Name == "Men");
            var women = categoryService.GetSingleCategory(x => x.Name == "Women");
            var kids = categoryService.GetSingleCategory(x => x.Name == "Kids");
            categories.AddRange([men,women,kids]);
            dataDictionary.Add("categories", categories);

            //Categories for the sport collection categories section
            //Outdoor,Training,Running,Fitness,Wintersport
            var sportCollectionCategories = new List<Category>();
            var sportCollectionCategoriesList = new List<string> { "Outdoor", "Training", "Running", "Fitness", "Wintersport" };
            foreach (var category in sportCollectionCategoriesList)
            {
                var result = categoryService.GetSingleCategory(x => x.Name == category);
                sportCollectionCategories.Add(result);
            }
            dataDictionary.Add("sportCollectionCategories", sportCollectionCategories);


            return dataDictionary;
        }
    }
}
