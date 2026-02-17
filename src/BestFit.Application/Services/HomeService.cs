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
        private readonly ProductService productService;

        public HomeService(FeaturedContentService featuredContentService,IMapper mapper,CategoryService categoryService,ProductService productService)
        {
            this.featuredContentService = featuredContentService;
            this.mapper = mapper;
            this.categoryService = categoryService;
            this.productService = productService;
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

            //Categories for the Popular  Categories Section
            //Jackets,Bags,Outdoor,Winterski,Sport
            var popularCategories = new List<Category>();
            var popularCategoriesList = new List<string> { "Jackets", "Bags", "Outdoor", "Winterski", "Sport" };
            foreach (var category in popularCategoriesList)
            {
                var result = categoryService.GetSingleCategory(x => x.Name == category);
                popularCategories.Add(result);
            }
            dataDictionary.Add("popularCategories", popularCategories);



            //Products for Black Friday Sale
            //Coretta,Tonya,Raven,Mufi
            var products = new List<Product>();
            var popularList = new List<string> { "Coretta", "Tonya", "Raven", "Mufi" };
            foreach (var category in popularList)
            {
                var result = productService.GetSingleProduct(x => x.Name == category);
                products.Add(result);
            }
            dataDictionary.Add("blackFridaySales", products);


            return dataDictionary;
        }
    }
}
