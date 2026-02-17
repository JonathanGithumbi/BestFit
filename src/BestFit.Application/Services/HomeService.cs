using AutoMapper;
using BestFit.Application.DTOs.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BestFit.Application.DTOs.ResponseDTOs;

namespace BestFit.Application.Services
{
    public class HomeService
    {
        private readonly FeaturedContentService featuredContentService;
        private readonly IMapper mapper;

        public HomeService(FeaturedContentService featuredContentService,IMapper mapper)
        {
            this.featuredContentService = featuredContentService;
            this.mapper = mapper;
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



            return dataDictionary;
        }
    }
}
